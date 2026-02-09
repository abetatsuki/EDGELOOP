
using UnityEngine;
namespace Runtime
{
    public class Movement : IJumpInputPort
    {
        public void Handle(JumpInputData data)
        {
            float power = _characterEntity.ConsueJumpPower();
            if (data.IsPressed)
            {
                _jumpPhysicsOutput.ApplyJump(new JumpCommand(power));
            }
            else
            {
                
            }
        }

        private CharacterEntity _characterEntity;
        private IJumpPhysicsOutput _jumpPhysicsOutput;
    }

}