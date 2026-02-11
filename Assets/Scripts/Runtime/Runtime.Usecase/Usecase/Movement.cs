
using System;

namespace Runtime
{
    public class Movement : IJumpInputPort, IMoveInputPort, IDashInputPort
    {
        private readonly CharacterEntity _characterEntity;
        private readonly IJumpPhysicsOutput _jumpPhysicsOutput;
        private readonly IMovePhysicsOutput _movePhysicsOutput;
        private readonly IDashPhysicsOutput _dashPhysicsOutput;
        private readonly IControlRotationReader _controlRotationReader;
        private CharacterConfigData _characterConfigData;

        public Movement(
            CharacterEntity characterEntity,
            CharacterConfigData characterConfigData,
            IJumpPhysicsOutput jumpPhysicsOutput,
            IMovePhysicsOutput movePhysicsOutput,
            IDashPhysicsOutput dashPhysicsOutput,
            IControlRotationReader controlRotationReader)
        {
            _characterEntity = characterEntity;
            _characterConfigData = characterConfigData;
            _jumpPhysicsOutput = jumpPhysicsOutput;
            _movePhysicsOutput = movePhysicsOutput;
            _dashPhysicsOutput = dashPhysicsOutput;
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

            float yawRad = _controlRotationReader.GetControlRotation().Yaw * (MathF.PI / 180f);
            float cos = MathF.Cos(yawRad);
            float sin = MathF.Sin(yawRad);

            float worldX = (localX * cos) + (localY * sin);
            float worldY = (-localX * sin) + (localY * cos);

            _movePhysicsOutput.ApplyMove(new MoveCommand(worldX, worldY, moveSpeed));
        }

        public void Handle(DashInputData data)
        {
           _characterEntity.SetIsDashing(data.IsPressed);
        }
    }
}

