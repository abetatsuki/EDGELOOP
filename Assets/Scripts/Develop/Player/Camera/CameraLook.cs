using Develop.Interface;
using UnityEngine;
namespace Develop.Player.Camera
{
    public class CameraLook : ILook
    {
        public CameraLook(IMovableBody bodyReference, float speed, float maxPitch)
        {
            _bodyReference = bodyReference;
            _speed = speed;
            _maxPitch = maxPitch;

            // 初期ピッチ角を現在のカメラ回転から取得
            Vector3 euler = _bodyReference.CameraQuaternion.eulerAngles;
            _pitch = euler.x;
            if (_pitch > 180) _pitch -= 360;
        }

        public void Look(Vector2 input)
        {
            float x = input.x * _speed;
            float y = input.y * _speed;

            // 垂直回転（ピッチ）の計算
            _pitch -= y;
            _pitch = Mathf.Clamp(_pitch, -_maxPitch, _maxPitch);

            // 水平回転をボディに適用（現在の回転に加算）
            _bodyReference.PlayerQuaternion *= Quaternion.Euler(0, x, 0);

            // 垂直回転をカメラに適用
            _bodyReference.CameraQuaternion = Quaternion.Euler(_pitch, 0, 0);

        }

        private readonly IMovableBody _bodyReference;
        private readonly float _speed;
        private float _pitch;
        private readonly float _maxPitch;
    }
}
