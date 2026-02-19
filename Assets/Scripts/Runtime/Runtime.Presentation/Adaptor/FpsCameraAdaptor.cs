using UnityEngine;
#if DOTWEEN_ENABLED
using DG.Tweening;
#endif

namespace Runtime
{
    public class FpsCameraAdaptor : MonoBehaviour, ICameraRotationOutput, IWallRunCameraOutput
    {
        [SerializeField] private Transform _bodyTransform;
        [SerializeField] private Transform _cameraTransform;
        [SerializeField] private float _wallRunFov = 95f;
        [SerializeField] private float _wallRunTiltAngle = 15f;
        [SerializeField] private float _wallRunTweenDuration = 0.25f;

        private Camera _cameraComponent;
        private float _defaultFov;
        private float _currentPitch;
        private float _currentTiltZ;
        private bool _isWallRunActive;
        private float _tiltSign;
#if DOTWEEN_ENABLED
        private Tween _fovTween;
        private Tween _tiltTween;
#endif

        public void ApplyLook(CameraLookCommand command)
        {
            if (_bodyTransform != null)
            {
                _bodyTransform.rotation = Quaternion.Euler(0f, command.Yaw, 0f);
            }

            if (_cameraTransform != null)
            {
                _currentPitch = command.Pitch;
                _cameraTransform.localRotation = Quaternion.Euler(_currentPitch, 0f, _currentTiltZ);
            }
        }

        public void ApplyWallRunCamera(WallRunCameraCommand command)
        {
            bool isSameState = _isWallRunActive == command.IsWallRunning;
            bool isSameTilt = Mathf.Abs(_tiltSign - command.TiltSign) <= 0.01f;
            if (isSameState && isSameTilt)
            {
                return;
            }

            _isWallRunActive = command.IsWallRunning;
            _tiltSign = command.TiltSign;

            float targetFov = _isWallRunActive ? _wallRunFov : _defaultFov;
            float targetTilt = _isWallRunActive ? _wallRunTiltAngle * _tiltSign : 0f;
            AnimateCamera(targetFov, targetTilt);
        }

        private void Awake()
        {
            if (_bodyTransform == null)
            {
                _bodyTransform = transform;
            }

            if (_cameraTransform == null && Camera.main != null)
            {
                _cameraTransform = Camera.main.transform;
            }

            if (_cameraTransform != null)
            {
                _cameraComponent = _cameraTransform.GetComponent<Camera>();
                _currentPitch = _cameraTransform.localEulerAngles.x;
            }
            else if (Camera.main != null)
            {
                _cameraComponent = Camera.main;
            }

            if (_cameraComponent != null)
            {
                _defaultFov = _cameraComponent.fieldOfView;
            }
        }

        private void AnimateCamera(float targetFov, float targetTilt)
        {
            if (_cameraComponent != null)
            {
#if DOTWEEN_ENABLED
                _fovTween?.Kill();
                _fovTween = _cameraComponent.DOFieldOfView(targetFov, _wallRunTweenDuration);
#else
                _cameraComponent.fieldOfView = targetFov;
#endif
            }

#if DOTWEEN_ENABLED
            _tiltTween?.Kill();
            _tiltTween = DOTween.To(
                () => _currentTiltZ,
                value =>
                {
                    _currentTiltZ = value;
                    if (_cameraTransform != null)
                    {
                        _cameraTransform.localRotation = Quaternion.Euler(_currentPitch, 0f, _currentTiltZ);
                    }
                },
                targetTilt,
                _wallRunTweenDuration);
#else
            _currentTiltZ = targetTilt;
            if (_cameraTransform != null)
            {
                _cameraTransform.localRotation = Quaternion.Euler(_currentPitch, 0f, _currentTiltZ);
            }
#endif
        }

        private void OnDestroy()
        {
#if DOTWEEN_ENABLED
            _fovTween?.Kill();
            _tiltTween?.Kill();
#endif
        }
    }
}
