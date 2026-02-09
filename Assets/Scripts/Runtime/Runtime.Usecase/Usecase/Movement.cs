

namespace Runtime
{
    public class Movement : IJumpInputPort
    {
        private readonly CharacterEntity _characterEntity;
        private readonly IJumpPhysicsOutput _jumpPhysicsOutput;

        public Movement(CharacterEntity characterEntity, IJumpPhysicsOutput jumpPhysicsOutput)
        {
            _characterEntity = characterEntity;
            _jumpPhysicsOutput = jumpPhysicsOutput;
        }

        public void Handle(JumpInputData data)
        {
            float power = 10f; // Example fixed jump power
            if (!_characterEntity.CanJump())
            {
                return;
            }
            if (data.IsPressed)
            {
                _jumpPhysicsOutput.ApplyJump(new JumpCommand(power));
            }
            else
            {
                
            }
        }
    }
}
