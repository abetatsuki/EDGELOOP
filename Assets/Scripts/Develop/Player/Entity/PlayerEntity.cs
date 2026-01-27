using Develop.Interface;
using Unity.VisualScripting;
using UnityEngine;

namespace Develop.Player.Entity
{
    public class PlayerEntity
    {
        public PlayerEntity()
        {

        }
        public bool IsJumping { get; private set; }
        public bool IsSliding { get; private set; }

        public bool CanMove()
        {
            return !IsJumping && !IsSliding;
        }

        public bool CanSliding()
        {
            return !IsJumping && IsSliding;
        }

        // ジャンプ開始
        public void StartJump()
        {
            IsJumping = true;
        }

        // 着地
        public void Land()
        {
            IsJumping = false;
        }
        public void StartSliding()
        {
            IsSliding = true;
        }

        public void StopSliding()
        {
            IsSliding = false;
        }
    }

}


