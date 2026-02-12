using UnityEngine;
namespace Runtime
{
    public class CharacterEntity
    {

        public bool CanJump()
        {
            return _isGround || _isWallRunning;
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
        public bool IsWallRunning()
        {
            return _isWallRunning;
        }
        public bool IsWallOnLeftSide()
        {
            return _wallSide == WallSide.Left;
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
        public void SetWallRun(bool isWallRunning, WallSide wallSide)
        {
            _isWallRunning = isWallRunning;
            _wallSide = wallSide;
        }
        private bool _isGround;
        private bool _isDashing;
        private bool _isCrouching;
        private bool _isSliding;
        private bool _isWallRunning;
        private WallSide _wallSide;

    }
}
