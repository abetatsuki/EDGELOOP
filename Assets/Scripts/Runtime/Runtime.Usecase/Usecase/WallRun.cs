using System;
using System.Collections.Generic;
using UnityEngine;

namespace Runtime
{
    public class WallRun : IWallRunInputPort, IWallSenseInputPort, IWallRunTickInputPort, IControlRotationOutput
    {
        private readonly CharacterConfigData _characterConfigData;
        private readonly IWallRunPhysicsOutput _wallRunPhysicsOutput;
        private readonly IReadOnlyList<IWallRunCameraOutput> _wallRunCameraOutputs;

        private WallRunInputData _lastInput;
        private WallSenseData _lastSense;
        private ControlRotationData _controlRotationData;
        private float _wallRunTimer;
        private bool _isWallRunning;

        public WallRun(
            CharacterConfigData characterConfigData,
            IWallRunPhysicsOutput wallRunPhysicsOutput,
            IReadOnlyList<IWallRunCameraOutput> wallRunCameraOutputs)
        {
            _characterConfigData = characterConfigData;
            _wallRunPhysicsOutput = wallRunPhysicsOutput;
            _wallRunCameraOutputs = wallRunCameraOutputs;
        }

        public void Handle(WallRunInputData data)
        {
            _lastInput = data;
        }

        public void Publish(WallSenseData data)
        {
            _lastSense = data;
        }

        public void Publish(ControlRotationData data)
        {
            _controlRotationData = data;
        }

        public void Tick(float deltaTime)
        {
            bool hasWall = _lastSense.HasLeftWall || _lastSense.HasRightWall;
            bool canStartByInput = _lastInput.MoveY > 0f;
            bool isEligibleByState = hasWall && canStartByInput && _lastSense.IsAboveGround;

            if (!isEligibleByState)
            {
                StopWallRun(resetTimer: true);
                _wallRunPhysicsOutput.ApplyWallRun(CreateStopCommand());
                PublishCamera(false, 0f);
                return;
            }

            bool canRunByTime = _characterConfigData.MaxWallRunTime <= 0f || _wallRunTimer < _characterConfigData.MaxWallRunTime;
            if (!canRunByTime)
            {
                StopWallRun(resetTimer: false);
                _wallRunPhysicsOutput.ApplyWallRun(CreateStopCommand());
                PublishCamera(false, 0f);
                return;
            }

            _isWallRunning = true;
            _wallRunTimer += Math.Max(0f, deltaTime);
            if (_characterConfigData.MaxWallRunTime > 0f)
            {
                _wallRunTimer = Math.Min(_wallRunTimer, _characterConfigData.MaxWallRunTime);
            }

            Vector3 wallNormal = _lastSense.HasRightWall ? _lastSense.RightWallNormal : _lastSense.LeftWallNormal;
            Vector3 wallForward = Vector3.Cross(wallNormal, Vector3.up);

            float yawRad = _controlRotationData.Yaw * (MathF.PI / 180f);
            Vector3 orientationForward = new Vector3(MathF.Sin(yawRad), 0f, MathF.Cos(yawRad));
            if ((orientationForward - wallForward).sqrMagnitude > (orientationForward + wallForward).sqrMagnitude)
            {
                wallForward = -wallForward;
            }

            float verticalVelocity = 0f;
            if (_lastInput.IsClimbPressed)
            {
                verticalVelocity = _characterConfigData.WallClimbSpeed;
            }
            else if (_lastInput.IsDescendPressed)
            {
                verticalVelocity = -_characterConfigData.WallClimbSpeed;
            }

            bool shouldStickToWall = !(_lastSense.HasLeftWall && _lastInput.MoveX > 0f) &&
                                    !(_lastSense.HasRightWall && _lastInput.MoveX < 0f);
            float tiltSign = _lastSense.HasRightWall ? 1f : -1f;

            _wallRunPhysicsOutput.ApplyWallRun(
                new WallRunCommand(
                    true,
                    false,
                    wallForward,
                    _characterConfigData.WallRunForce,
                    true,
                    verticalVelocity,
                    shouldStickToWall,
                    wallNormal,
                    _characterConfigData.WallStickForce));
            PublishCamera(true, tiltSign);
        }

        private void StopWallRun(bool resetTimer)
        {
            _isWallRunning = false;
            if (resetTimer)
            {
                _wallRunTimer = 0f;
            }
        }

        private WallRunCommand CreateStopCommand()
        {
            return new WallRunCommand(
                false,
                true,
                Vector3.zero,
                0f,
                false,
                0f,
                false,
                Vector3.zero,
                0f);
        }

        private void PublishCamera(bool isWallRunning, float tiltSign)
        {
            WallRunCameraCommand command = new WallRunCameraCommand(isWallRunning, tiltSign);
            for (int i = 0; i < _wallRunCameraOutputs.Count; i++)
            {
                _wallRunCameraOutputs[i].ApplyWallRunCamera(command);
            }
        }
    }
}
