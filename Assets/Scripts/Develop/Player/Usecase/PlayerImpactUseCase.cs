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

        private readonly Transform _gunImpactTransform; // 追加
        private readonly float _gunImpactForce; // 追加
        private readonly Vector3 _gunImpactOffset; // 追加


        public PlayerImpactUseCase(
            CinemachineImpulseSource impulseSource,
            Transform playerTransform,
            float impactStrength,
            float diagonalThreshold,
            Transform gunImpactTransform, // 追加
            float gunImpactForce, // 追加
            Vector3 gunImpactOffset) // 追加
        {
            _impulseSource = impulseSource;
            _playerTransform = playerTransform;
            _impactStrength = impactStrength;
            _diagonalThreshold = diagonalThreshold;
            _gunImpactTransform = gunImpactTransform; // 初期化
            _gunImpactForce = gunImpactForce; // 初期化
            _gunImpactOffset = gunImpactOffset; // 初期化
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
                // 銃の衝撃（真上/真下からの衝撃）
                if (_gunImpactTransform != null)
                {
                    _gunImpactTransform.localPosition += _gunImpactOffset * 0.5f;
                }
                Debug.Log("真上/真下からの被弾！ (デフォルト衝撃)");
                return;
            }
            attackDirFlat.Normalize();

            float dotForward = Vector3.Dot(playerForward, attackDirFlat);
            float dotRight = Vector3.Dot(playerRight, attackDirFlat);

            Vector3 impulseVelocity = Vector3.zero;
            Vector3 gunImpactDirection = Vector3.zero; // 銃の衝撃方向

            if (dotForward > _diagonalThreshold)
            {
                impulseVelocity = playerForward * _impactStrength;
                gunImpactDirection = -_playerTransform.forward; // 後ろから撃たれたら銃は手前に揺れる
                Debug.Log("後ろから被弾！ (前方向へ)");
            }
            else if (dotForward < -_diagonalThreshold)
            {
                impulseVelocity = playerForward * -1 * _impactStrength;
                gunImpactDirection = _playerTransform.forward; // 前から撃たれたら銃は奥に揺れる
                Debug.Log("前から被弾！ (後ろ方向へ)");
            }
            else if (dotRight > _diagonalThreshold)
            {
                impulseVelocity = playerRight * -1 * _impactStrength;
                gunImpactDirection = _playerTransform.right; // 右から撃たれたら銃は右に揺れる
                Debug.Log("右から被弾！ (左方向へ)");
            }
            else if (dotRight < -_diagonalThreshold)
            {
                impulseVelocity = playerRight * _impactStrength;
                gunImpactDirection = -_playerTransform.right; // 左から撃たれたら銃は左に揺れる
                Debug.Log("左から被弾！ (右方向へ)");
            }
            else
            {
                impulseVelocity = -attackDirFlat * _impactStrength * 0.5f;
                gunImpactDirection = -attackDirFlat; // 斜めからの攻撃はノックバック
                Debug.Log("斜めからの被弾！ (ノックバック)");
            }

            if (impulseVelocity != Vector3.zero)
            {
                _impulseSource.GenerateImpulseWithVelocity(impulseVelocity);
            }

            // 銃の衝撃を適用 (ローカル位置を瞬間的にずらす)
            if (_gunImpactTransform != null && gunImpactDirection != Vector3.zero)
            {
                // ここでは単純にlocalPositionをずらすだけ。
                // 継続的な動きやSmoothDampで戻す処理は、PlayerImpactUseCaseがUpdate()を持たないため
                // 別のメカニズムが必要になるが、今回は単発のズレに留める。
                _gunImpactTransform.localPosition += gunImpactDirection * _gunImpactForce + _gunImpactOffset;
            }
        }
    }
}
