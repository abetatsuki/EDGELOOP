
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
        private MoveInputData _lastMoveInput;

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
            _lastMoveInput = data;
            float magnitude = MathF.Sqrt((data.X * data.X) + (data.Y * data.Y));
            float speedScale = MathF.Min(1f, magnitude);

            float moveSpeed = _characterConfigData.MoveSpeed * speedScale;
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
            if (!data.IsPressed)
            {
                return;
            }

            float magnitude = MathF.Sqrt((_lastMoveInput.X * _lastMoveInput.X) + (_lastMoveInput.Y * _lastMoveInput.Y));
            if (magnitude <= 0.0001f)
            {
                return;
            }

            float x = _lastMoveInput.X / magnitude;
            float y = _lastMoveInput.Y / magnitude;
            _dashPhysicsOutput.ApplyDash(new DashCommand(x, y, _characterConfigData.DashPower));
        }
    }
}
