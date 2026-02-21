
using System;
using System.Collections.Generic;
using UnityEngine;
namespace Runtime
{
    public class Movement : IJumpInputPort, IMoveInputPort, IDashInputPort, ISlideInputPort, IRunSpeedInputPort, IControlRotationOutput, IWallSenseInputPort
    {
        private readonly CharacterEntity _characterEntity;
        private readonly IJumpPhysicsOutput _jumpPhysicsOutput;
        private readonly IMovePhysicsOutput _movePhysicsOutput;
        private readonly IDashPhysicsOutput _dashPhysicsOutput;
        private readonly ISlideMotionOutput _slideMotionOutput;
        private readonly IReadOnlyList<IMoveSpeedOutput> _moveSpeedOutputs;
        private CharacterConfigData _characterConfigData;
        private MoveInputData _lastMoveInput;
        private WallSenseData _lastWallSense;
        private ControlRotationData _controlRotationData;
        private const float INPUT_THRESHOLD = 0.0001f;
        private const float RUN_MAX_RATIO = 1f;
        private const float WALL_JUMP_HORIZONTAL_RATIO = 0.8f;
        private const float WALL_RUN_RELOCK_SECONDS = 0.25f;
        private float _runSpeedRatio = RUN_MAX_RATIO;

        public Movement(
            CharacterEntity characterEntity,
            CharacterConfigData characterConfigData,
            IJumpPhysicsOutput jumpPhysicsOutput,
            IMovePhysicsOutput movePhysicsOutput,
            IDashPhysicsOutput dashPhysicsOutput,
            ISlideMotionOutput slideMotionOutput,
            IReadOnlyList<IMoveSpeedOutput> moveSpeedOutputs)
        {
            _characterEntity = characterEntity;
            _characterConfigData = characterConfigData;
            _jumpPhysicsOutput = jumpPhysicsOutput;
            _movePhysicsOutput = movePhysicsOutput;
            _dashPhysicsOutput = dashPhysicsOutput;
            _slideMotionOutput = slideMotionOutput;
            _moveSpeedOutputs = moveSpeedOutputs;
        }

        public void Handle(JumpInputData data)
        {
            if (!data.IsPressed)
            {
                return;
            }

            if (_characterEntity.CanJump())
            {
                _jumpPhysicsOutput.ApplyJump(new JumpCommand(_characterConfigData.JumpPower));
                return;
            }

            if (!CanWallJump())
            {
                return;
            }

            Vector3 awayDirection = ResolveWallNormal();
            if (awayDirection.sqrMagnitude <= INPUT_THRESHOLD)
            {
                return;
            }

            awayDirection = awayDirection.normalized;
            Vector3 inputDirection = ResolveWorldInputDirection();
            Vector3 jumpDirection = ResolveWallJumpDirection(inputDirection, awayDirection);

            float horizontalPower = _characterConfigData.DashPower * WALL_JUMP_HORIZONTAL_RATIO;
            _jumpPhysicsOutput.ApplyJump(new JumpCommand(0f, jumpDirection, horizontalPower));
            _characterEntity.LockWallRun(WALL_RUN_RELOCK_SECONDS);
        }

        public void Handle(MoveInputData data)
        {
            _lastMoveInput = data;

            float magnitude = MathF.Sqrt((data.X * data.X) + (data.Y * data.Y));
            float speedScale = MathF.Min(1f, magnitude);
            float runSpeed = _characterConfigData.MoveSpeed * _runSpeedRatio;
            float baseSpeed = _characterEntity.IsSprinting() ? _characterConfigData.DashPower : runSpeed;
            float moveSpeed = baseSpeed * speedScale;
            float localX = 0f;
            float localY = 0f;
            if (magnitude > 0.0001f)
            {
                localX = data.X / magnitude;
                localY = data.Y / magnitude;
            }

            ToWorldDirection(localX, localY, out float worldX, out float worldY);

            _movePhysicsOutput.ApplyMove(new MoveCommand(worldX, worldY, moveSpeed));
            MoveSpeedData speedData = new MoveSpeedData(moveSpeed, runSpeed, _characterConfigData.DashPower);
            for (int i = 0; i < _moveSpeedOutputs.Count; i++)
            {
                _moveSpeedOutputs[i].Publish(speedData);
            }
        }

        public void Handle(DashInputData data)
        {
           _characterEntity.SetIsDashing(data.IsPressed);
        }

        public void Handle(RunSpeedInputData data)
        {
            if (MathF.Abs(data.Delta) <= INPUT_THRESHOLD)
            {
                return;
            }

            _runSpeedRatio = Math.Clamp(
                _runSpeedRatio + (data.Delta * Math.Max(0f, _characterConfigData.RunScrollStep)),
                Math.Clamp(_characterConfigData.RunMinRatio, 0f, RUN_MAX_RATIO),
                RUN_MAX_RATIO);
        }

        public void Handle(SlideInputData data)
        {
            if (!data.IsPressed)
            {
                _characterEntity.SetIsSliding(false);
                _characterEntity.SetIsCrouching(false);
                _slideMotionOutput.ApplySlideMotion(new SlideMotionCommand(SlideMode.Stand, 0f, 0f, 0f));
                return;
            }

            float magnitude = MathF.Sqrt((_lastMoveInput.X * _lastMoveInput.X) + (_lastMoveInput.Y * _lastMoveInput.Y));
            if (magnitude > INPUT_THRESHOLD)
            {
                float localX = _lastMoveInput.X / magnitude;
                float localY = _lastMoveInput.Y / magnitude;
                ToWorldDirection(localX, localY, out float worldX, out float worldY);

                _characterEntity.SetIsSliding(true);
                _characterEntity.SetIsCrouching(false);
                _slideMotionOutput.ApplySlideMotion(
                    new SlideMotionCommand(SlideMode.Slide, worldX, worldY, _characterConfigData.DashPower));
                return;
            }

            _characterEntity.SetIsSliding(false);
            _characterEntity.SetIsCrouching(true);
            _slideMotionOutput.ApplySlideMotion(new SlideMotionCommand(SlideMode.Crouch, 0f, 0f, 0f));
        }

        public void Publish(ControlRotationData data)
        {
            _controlRotationData = data;
        }

        public void Publish(WallSenseData data)
        {
            _lastWallSense = data;
        }

        private void ToWorldDirection(float localX, float localY, out float worldX, out float worldY)
        {
            float yawRad = _controlRotationData.Yaw * (MathF.PI / 180f);
            float cos = MathF.Cos(yawRad);
            float sin = MathF.Sin(yawRad);

            worldX = (localX * cos) + (localY * sin);
            worldY = (-localX * sin) + (localY * cos);
        }

        private bool CanWallJump()
        {
            if (!_lastWallSense.IsAboveGround)
            {
                return false;
            }
            return _lastWallSense.HasLeftWall || _lastWallSense.HasRightWall;
        }

        private Vector3 ResolveWallNormal()
        {
            bool hasLeft = _lastWallSense.HasLeftWall;
            bool hasRight = _lastWallSense.HasRightWall;

            if (hasLeft && hasRight)
            {
                Vector3 inputDir = ResolveWorldInputDirection();
                float leftDot = Vector3.Dot(inputDir, _lastWallSense.LeftWallNormal);
                float rightDot = Vector3.Dot(inputDir, _lastWallSense.RightWallNormal);
                return leftDot >= rightDot ? _lastWallSense.LeftWallNormal : _lastWallSense.RightWallNormal;
            }

            if (hasRight)
            {
                return _lastWallSense.RightWallNormal;
            }

            if (hasLeft)
            {
                return _lastWallSense.LeftWallNormal;
            }

            return Vector3.zero;
        }

        private Vector3 ResolveWorldInputDirection()
        {
            float magnitude = MathF.Sqrt((_lastMoveInput.X * _lastMoveInput.X) + (_lastMoveInput.Y * _lastMoveInput.Y));
            if (magnitude <= INPUT_THRESHOLD)
            {
                return Vector3.zero;
            }

            float localX = _lastMoveInput.X / magnitude;
            float localY = _lastMoveInput.Y / magnitude;
            ToWorldDirection(localX, localY, out float worldX, out float worldY);
            return new Vector3(worldX, 0f, worldY);
        }

        private Vector3 ResolveWallJumpDirection(Vector3 inputDirection, Vector3 awayDirection)
        {
            if (inputDirection.sqrMagnitude <= INPUT_THRESHOLD)
            {
                return awayDirection;
            }

            Vector3 normalizedInput = inputDirection.normalized;
            float dot = Vector3.Dot(normalizedInput, awayDirection);
            if (dot <= 0f)
            {
                return awayDirection;
            }

            return normalizedInput;
        }
    }
}
