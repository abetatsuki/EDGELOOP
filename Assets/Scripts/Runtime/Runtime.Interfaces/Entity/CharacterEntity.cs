using UnityEngine;
namespace Runtime
{
    public class CharacterEntity
    {

        public bool CanJump()
        {
            return _isGround;
        }
        public bool CanDash()
        {
            return !_isDashing;
        }

        public void SetIsGround(bool isGround)
        {
            _isGround = isGround;
        }
        public void SetIsDashing(bool isDashing)
        {
            _isDashing = isDashing;
        }
        private bool _isGround;
        private bool _isDashing;

    }
}