using Develop.Interface;
using UnityEngine;

namespace Develop.Player.Move.Strategies
{
    public sealed class SlideStrategy : IMovementStrategy
    {
        public SlideStrategy(
            float decelerationRate,
            float endSpeed,
            LayerMask groundLayer,
            float groundCheckDistance)
        {
            _decelerationRate = decelerationRate;
            _endSpeed = endSpeed;
            _groundLayer = groundLayer;
            _groundCheckDistance = groundCheckDistance;
        }

        /// <summary>
        /// スライディング中の移動処理を行う
        /// </summary>
        public void Move(IMovableBody body, Vector2 input, float deltaTime)
        {
            // 地面法線を取得する
            Vector3 groundNormal = GetGroundNormal(body);

            // 現在の速度方向を地面に投影し、スライド方向を補正する
            Vector3 slideDirection = Vector3.ProjectOnPlane(body.Velocity, groundNormal).normalized;

            // 指数減衰による速度低下を計算する
            float speed = body.Velocity.magnitude;
            speed *= Mathf.Exp(-_decelerationRate * deltaTime);

            // Rigidbody にスライド速度を反映する
            body.Velocity = slideDirection * speed;
        }

        /// <summary>
        /// Raycast を使用して地面の法線を取得する
        /// </summary>
        private Vector3 GetGroundNormal(IMovableBody body)
        {

            Ray ray = new Ray(body.Position, Vector3.down);

            if (Physics.Raycast(
                ray,
                out RaycastHit hit,
                _groundCheckDistance,
                _groundLayer))
            {
                return hit.normal;
            }

            // 地面が取得できない場合は上向きを返す
            return Vector3.up;
        }

        private readonly float _decelerationRate;
        private readonly float _endSpeed;
        private readonly LayerMask _groundLayer;
        private readonly float _groundCheckDistance;
    }
}
