using UnityEngine;
namespace Runtime
{
    public class CharacterEntity
    {

        public bool CanJump()
        {
            return _isGround;
        }
        public bool IsSprinting()
        {
            return _isDashing;
        }
        public bool IsDashing()
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
        public bool IsGround()
        {
            return _isGround;
        }
        public bool IsWallRunLocked()
        {
            return _wallRunLockRemaining > 0f;
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
        public void LockWallRun(float duration)
        {
            _wallRunLockRemaining = Mathf.Max(_wallRunLockRemaining, Mathf.Max(0f, duration));
        }
        public void TickWallRunLock(float deltaTime)
        {
            if (_wallRunLockRemaining <= 0f)
            {
                return;
            }
            _wallRunLockRemaining = Mathf.Max(0f, _wallRunLockRemaining - Mathf.Max(0f, deltaTime));
        }
        private bool _isGround;
        private bool _isDashing;
        private bool _isCrouching;
        private bool _isSliding;
        private float _wallRunLockRemaining;

    }
}
