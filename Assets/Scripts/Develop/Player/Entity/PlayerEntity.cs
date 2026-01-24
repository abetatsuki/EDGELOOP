using UnityEngine;

namespace Develop.Player.Entity
{
    public class PlayerEntity 
    {
        public PlayerEntity()
        {

        }
        public bool IsJumping { get; private set; }


        // 移動できるかどうかを判定する
        public bool CanMove()
        {
            return !IsJumping;
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
    }

}


