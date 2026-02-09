using UnityEngine;
namespace Runtime
{
    public class CharacterEntity
    {
        public bool CanJump()
        {
            return _isGround;
        }

        public float ConsueJumpPower()
        {
            return 10f;
        }

        public void SetIsGround(bool isGround)
        {
            _isGround = isGround;
        }
        private bool _isGround;

    }
}