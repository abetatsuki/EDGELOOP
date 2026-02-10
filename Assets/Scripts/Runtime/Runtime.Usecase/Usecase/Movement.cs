
using System;

namespace Runtime
{
    public class Movement : IJumpInputPort, IMoveInputPort, IDashInputPort, ICharacterConfigPort
    {
        private readonly CharacterEntity _characterEntity;
        private readonly IJumpPhysicsOutput _jumpPhysicsOutput;
        private readonly IMovePhysicsOutput _movePhysicsOutput;
        private readonly IDashPhysicsOutput _dashPhysicsOutput;
        private CharacterConfigData _characterConfigData;

        public Movement(
            CharacterEntity characterEntity,
            IJumpPhysicsOutput jumpPhysicsOutput,
            IMovePhysicsOutput movePhysicsOutput,
            IDashPhysicsOutput dashPhysicsOutput)
        {
            _characterEntity = characterEntity;
            _jumpPhysicsOutput = jumpPhysicsOutput;
            _movePhysicsOutput = movePhysicsOutput;
            _dashPhysicsOutput = dashPhysicsOutput;
        }

        public void SetCharacterConfig(CharacterConfigData characterConfigData)
        {
            _characterConfigData = characterConfigData;
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
            float x = 0f;
            float y = 0f;
            if (magnitude > 0.0001f)
            {
                x = data.X / magnitude;
                y = data.Y / magnitude;
            }

            _movePhysicsOutput.ApplyMove(new MoveCommand(x, y, moveSpeed));
        }

        public void Handle(DashInputData data)
        {
           _characterEntity.SetIsDashing(data.IsPressed);
        }
    }
}
