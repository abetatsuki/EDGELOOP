using UnityEngine;

namespace Develop.Gun
{
    public class GunRecoilHandler
    {
        private readonly GunConfig _config;

        private Vector3 _currentRecoilRotation; // 現在のリコイル回転オフセット
        private Vector3 _currentRecoilPosition; // 現在のリコイル位置オフセット
        private Vector3 _recoilRotationVelocity; // SmoothDamp用の速度
        private Vector3 _recoilPositionVelocity; // SmoothDamp用の速度

        public GunRecoilHandler(GunConfig config)
        {
            _config = config;
        }

        public void ApplyRecoil()
        {
            // 発射された瞬間にリコイルを追加
            // ランダム性を加えることも可能
            _currentRecoilRotation += new Vector3(
                _config.RecoilRotationAmount.x + Random.Range(-0.1f, 0.1f), // 上方向のランダム性
                _config.RecoilRotationAmount.y + Random.Range(-0.1f, 0.1f), // 横方向のランダム性
                _config.RecoilRotationAmount.z
            );
            _currentRecoilPosition += _config.RecoilPositionAmount;

            // リコイル値の上限を適用 (x, y, z成分ごとにクランプ)
            _currentRecoilRotation.x = Mathf.Clamp(_currentRecoilRotation.x, -_config.MaxRecoilRotation.x, _config.MaxRecoilRotation.x);
            _currentRecoilRotation.y = Mathf.Clamp(_currentRecoilRotation.y, -_config.MaxRecoilRotation.y, _config.MaxRecoilRotation.y);
            _currentRecoilRotation.z = Mathf.Clamp(_currentRecoilRotation.z, -_config.MaxRecoilRotation.z, _config.MaxRecoilRotation.z);

            _currentRecoilPosition.x = Mathf.Clamp(_currentRecoilPosition.x, -_config.MaxRecoilPosition.x, _config.MaxRecoilPosition.x);
            _currentRecoilPosition.y = Mathf.Clamp(_currentRecoilPosition.y, -_config.MaxRecoilPosition.y, _config.MaxRecoilPosition.y);
            _currentRecoilPosition.z = Mathf.Clamp(_currentRecoilPosition.z, -_config.MaxRecoilPosition.z, _config.MaxRecoilPosition.z);
        }

        public void Update()
        {
            // リコイルの回転を目標の回転(0,0,0)に戻す
            _currentRecoilRotation = Vector3.SmoothDamp(
                _currentRecoilRotation,
                Vector3.zero,
                ref _recoilRotationVelocity,
                1 / _config.RecoilReturnSpeed,
                Mathf.Infinity, // MaxSpeedをInfinityにして、RecoilSpeedで制御
                Time.deltaTime
            );
            
            // リコイルの位置を目標の位置(0,0,0)に戻す
            _currentRecoilPosition = Vector3.SmoothDamp(
                _currentRecoilPosition,
                Vector3.zero,
                ref _recoilPositionVelocity,
                1 / _config.RecoilReturnSpeed,
                Mathf.Infinity, // MaxSpeedをInfinityにして、RecoilSpeedで制御
                Time.deltaTime
            );
        }

        // 現在のリコイルによる回転オフセットを取得
        public Quaternion GetRecoilRotationOffset() => Quaternion.Euler(_currentRecoilRotation);
        // 現在のリコイルによる位置オフセットを取得
        public Vector3 GetRecoilPositionOffset() => _currentRecoilPosition;
    }
}
