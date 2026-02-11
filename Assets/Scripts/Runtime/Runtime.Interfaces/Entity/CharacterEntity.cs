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
            return _isDashing;
        }
        public bool IsCrouching()
        {
            return _isCrouching;
        }
        public bool IsSliding()
        {
            return _isSliding;
        }

        public void SetIsGround(bool isGround)
        {
            _isGround = isGround;
        }
        public void SetIsDashing(bool isDashing)
        {
            _isDashing = isDashing;
        }
        public void SetIsCrouching(bool isCrouching)
        {
            _isCrouching = isCrouching;
        }
        public void SetIsSliding(bool isSliding)
        {
            _isSliding = isSliding;
        }
        private bool _isGround;
        private bool _isDashing;
        private bool _isCrouching;
        private bool _isSliding;

    }
}
