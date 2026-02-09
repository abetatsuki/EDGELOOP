using Develop.Interface;
using System; // Eventのために追加
using UnityEngine; // Physics.CheckSphereのために必要

namespace Develop.Player.Entity
{
    public class PlayerEntity
    {
        public PlayerEntity()
        {

        }
        public bool IsJumping { get; private set; }
        public bool IsSliding { get; private set; }
        public bool IsGrounded { get; private set; } // 追加

        public event Action<bool> OnSlidingStatusChanged; // 追加

        public bool CanMove()
        {
            return !IsJumping && !IsSliding;
        }

        public bool CanSliding()
        {
            return !IsJumping && IsSliding;
        }

        // 接地状態を更新
        public void UpdateGroundStatus(Vector3 playerPosition, LayerMask groundLayer, float groundCheckDistance)
        {
            // Physics.CheckSphereを使って接地判定を行う
            // playerPositionはプレイヤーのTransform.positionを想定。
            // 足元からの距離を考慮して球の原点を調整する必要がある場合もあるが、
            // 今回はシンプルにplayerPositionを基準とする
            IsGrounded = Physics.CheckSphere(playerPosition, groundCheckDistance, groundLayer);
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
            OnSlidingStatusChanged?.Invoke(true); // イベントを発行
        }

        public void StopSliding()
        {
            IsSliding = false;
            OnSlidingStatusChanged?.Invoke(false); // イベントを発行
        }
    }

}


