using Develop.Interface;
using Develop.Player.Entity;
using UnityEngine;

namespace Develop.Player.Move.Strategies
{
    public sealed class SlideStrategy : IMovementStrategy
    {
        private readonly IMovableBody _movableBody;
        private readonly PlayerEntity _playerEntity;
        private readonly PlayerConfig _config;

        private float _slideStartTime;

        public SlideStrategy(IMovableBody movableBody, PlayerEntity playerEntity, PlayerConfig config)
        {
            _movableBody = movableBody;
            _playerEntity = playerEntity;
            _config = config;
        }

        public void Enter(IMovableBody body, PlayerEntity playerEntity)
        {
            _playerEntity.StartSliding();
            body.ColliderHeight = _config.SlideColliderHeight;
            body.ColliderCenterY = _config.SlideColliderCenterY;
            _slideStartTime = Time.time;
            Debug.Log("Sliding Started!");
        }

        public void Execute(IMovableBody body, Vector2 input, float deltaTime)
        {
            // スライディング中は常に速度を前方へ設定
            body.Velocity = body.Forward * _config.SlideSpeed;

            // 時間経過でスライディングを終了
            if (Time.time - _slideStartTime >= _config.SlideDuration)
            {
                _playerEntity.StopSliding();
            }
        }

        public void Exit(IMovableBody body, PlayerEntity playerEntity)
        {
            _playerEntity.StopSliding(); // 念のため呼ぶ (入力解除時にも呼ばれるため重複する可能性あり)
            body.ColliderHeight = body.DefaultColliderHeight;
            body.ColliderCenterY = body.DefaultColliderCenterY;
            Debug.Log("Sliding Ended!");
        }

        // 古いフィールドは削除
        // private readonly float _decelerationRate;
        // private readonly float _endSpeed;
        // private readonly LayerMask _groundLayer;
        // private readonly float _groundCheckDistance;
    }
}
