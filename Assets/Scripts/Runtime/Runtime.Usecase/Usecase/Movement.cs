
using System;

namespace Runtime
{
    public class Movement : IJumpInputPort, IMoveInputPort, IDashInputPort, ISlideInputPort
    {
        private readonly CharacterEntity _characterEntity;
        private readonly IJumpPhysicsOutput _jumpPhysicsOutput;
        private readonly IMovePhysicsOutput _movePhysicsOutput;
        private readonly IDashPhysicsOutput _dashPhysicsOutput;
        private readonly ISlideMotionOutput _slideMotionOutput;
        private readonly IControlRotationReader _controlRotationReader;
        private CharacterConfigData _characterConfigData;
        private MoveInputData _lastMoveInput;
        private const float INPUT_THRESHOLD = 0.0001f;

        public Movement(
            CharacterEntity characterEntity,
            CharacterConfigData characterConfigData,
            IJumpPhysicsOutput jumpPhysicsOutput,
            IMovePhysicsOutput movePhysicsOutput,
            IDashPhysicsOutput dashPhysicsOutput,
            ISlideMotionOutput slideMotionOutput,
            IControlRotationReader controlRotationReader)
        {
            _characterEntity = characterEntity;
            _characterConfigData = characterConfigData;
            _jumpPhysicsOutput = jumpPhysicsOutput;
            _movePhysicsOutput = movePhysicsOutput;
            _dashPhysicsOutput = dashPhysicsOutput;
            _slideMotionOutput = slideMotionOutput;
            _controlRotationReader = controlRotationReader;
        }

        public void Handle(JumpInputData data)
        {
            if (!_characterEntity.CanJump())
            {
                return;
            }
            float power = _characterConfigData.JumpPower;
            if (data.IsPressed)
            {
                _jumpPhysicsOutput.ApplyJump(new JumpCommand(power));
            }
            else
            {
                
            }
        }

        public void Handle(MoveInputData data)
        {
            _lastMoveInput = data;

            float magnitude = MathF.Sqrt((data.X * data.X) + (data.Y * data.Y));
            float speedScale = MathF.Min(1f, magnitude);

            float baseSpeed = _characterEntity.CanDash() ? _characterConfigData.DashPower : _characterConfigData.MoveSpeed;
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
        }

        public void Handle(DashInputData data)
        {
           _characterEntity.SetIsDashing(data.IsPressed);
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

        private void ToWorldDirection(float localX, float localY, out float worldX, out float worldY)
        {
            float yawRad = _controlRotationReader.GetControlRotation().Yaw * (MathF.PI / 180f);
            float cos = MathF.Cos(yawRad);
            float sin = MathF.Sin(yawRad);

            worldX = (localX * cos) + (localY * sin);
            worldY = (-localX * sin) + (localY * cos);
        }
    }
}

