using UnityEngine;
using Develop.Interface;
using Develop.Player.Entity;

namespace Develop.Player.Usecase
{
    public class JumpPlayerUseCase
    {
        private readonly IMovableBody _movableBody;
        private readonly PlayerEntity _playerEntity;
        private readonly PlayerConfig _config;

        private int _currentJumps; // 現在のジャンプ回数

        public JumpPlayerUseCase(IMovableBody movableBody, PlayerEntity playerEntity, PlayerConfig config)
        {
            _movableBody = movableBody;
            _playerEntity = playerEntity;
            _config = config;
            _currentJumps = 0; // 初期化
        }

        public void Jump()
        {
            // 接地しているか、または多段ジャンプが可能か
            if (_playerEntity.IsGrounded || _currentJumps < _config.MaxJumps)
            {
                // ジャンプ開始のフラグを立てる
                _playerEntity.StartJump();
                _currentJumps++;

                // RigidbodyのY軸速度をリセットしてから力を加えることで、ジャンプの高さを一定にする
                Vector3 currentVelocity = _movableBody.Velocity;
                _movableBody.Velocity = new Vector3(currentVelocity.x, 0f, currentVelocity.z);
                
                _movableBody.Rigidbody.AddForce(Vector3.up * _config.JumpForce, ForceMode.Impulse);
                
                Debug.Log($"Jump! Current Jumps: {_currentJumps}");
            }
            else
            {
                Debug.Log("Can't jump: Not grounded or no jumps left.");
            }
        }

        // 接地状態を更新し、着地したかを判定する
        public void UpdateGroundStatus()
        {
            // プレイヤーの位置を足元に合わせて接地判定を行う
            Vector3 playerFootPosition = _movableBody.Position - Vector3.up * 0.9f; // プレイヤーの高さに応じて調整
            _playerEntity.UpdateGroundStatus(playerFootPosition, _config.GroundLayer, _config.GroundCheckDistance);

            // 着地した瞬間を検出
            if (_playerEntity.IsGrounded && _playerEntity.IsJumping)
            {
                _playerEntity.Land();
                _currentJumps = 0; // ジャンプ回数をリセット
                Debug.Log("Landed! Jumps reset.");
            }
        }
    }
}
