using UnityEngine;
using Unity.Cinemachine;

namespace Develop.Player.Usecase
{
    public class PlayerImpactUseCase
    {
        private readonly CinemachineImpulseSource _impulseSource;
        private readonly Transform _playerTransform;
        private readonly float _impactStrength;
        private readonly float _diagonalThreshold;

        public PlayerImpactUseCase(
            CinemachineImpulseSource impulseSource,
            Transform playerTransform,
            float impactStrength,
            float diagonalThreshold)
        {
            _impulseSource = impulseSource;
            _playerTransform = playerTransform;
            _impactStrength = impactStrength;
            _diagonalThreshold = diagonalThreshold;
        }

        public void GenerateImpact(Vector3 attackDirection)
        {
            if (_impulseSource == null) return;

            Vector3 playerForward = _playerTransform.forward;
            playerForward.y = 0;
            playerForward.Normalize();

            Vector3 playerRight = _playerTransform.right;
            playerRight.y = 0;
            playerRight.Normalize();

            Vector3 attackDirFlat = attackDirection;
            attackDirFlat.y = 0;
            if (attackDirFlat.sqrMagnitude == 0)
            {
                _impulseSource.GenerateImpulseWithVelocity(-attackDirection.normalized * _impactStrength * 0.5f);
                Debug.Log("真上/真下からの被弾！ (デフォルト衝撃)");
                return;
            }
            attackDirFlat.Normalize();

            float dotForward = Vector3.Dot(playerForward, attackDirFlat);
            float dotRight = Vector3.Dot(playerRight, attackDirFlat);

            Vector3 impulseVelocity = Vector3.zero;

            if (dotForward > _diagonalThreshold)
            {
                impulseVelocity = playerForward * _impactStrength;
                Debug.Log("後ろから被弾！ (前方向へ)");
            }
            else if (dotForward < -_diagonalThreshold)
            {
                impulseVelocity = playerForward * -1 * _impactStrength;
                Debug.Log("前から被弾！ (後ろ方向へ)");
            }
            else if (dotRight > _diagonalThreshold)
            {
                impulseVelocity = playerRight * -1 * _impactStrength;
                Debug.Log("右から被弾！ (左方向へ)");
            }
            else if (dotRight < -_diagonalThreshold)
            {
                impulseVelocity = playerRight * _impactStrength;
                Debug.Log("左から被弾！ (右方向へ)");
            }
            else
            {
                impulseVelocity = -attackDirFlat * _impactStrength * 0.5f;
                Debug.Log("斜めからの被弾！ (ノックバック)");
            }

            if (impulseVelocity != Vector3.zero)
            {
                _impulseSource.GenerateImpulseWithVelocity(impulseVelocity);
            }
        }
    }
}
