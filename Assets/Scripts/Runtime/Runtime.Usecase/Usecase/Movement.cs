

using UnityEngine;

namespace Runtime
{
    public class Movement : IJumpInputPort, ICharacterConfigPort
    {
        private readonly CharacterEntity _characterEntity;
        private readonly IJumpPhysicsOutput _jumpPhysicsOutput;
        private  CharacterConfigData _characterConfigData;

        public Movement(CharacterEntity characterEntity, IJumpPhysicsOutput jumpPhysicsOutput)
        {
            _characterEntity = characterEntity;
            _jumpPhysicsOutput = jumpPhysicsOutput;
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
    }
}
