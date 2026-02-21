using UnityEngine;
#if DOTWEEN_ENABLED
using DG.Tweening;
#endif

namespace Runtime
{
    public class FpsCameraAdaptor : MonoBehaviour, ICameraRotationOutput, IWallRunCameraOutput
    {
        [SerializeField] private Transform _bodyTransform;
        [SerializeField] private Transform _pitchPivot;
        [SerializeField] private Transform _cameraTransform;
        [SerializeField] private GameObject[] _localOnlyObjects;
        [SerializeField] private float _wallRunFov = 95f;
        [SerializeField] private float _wallRunTiltAngle = 15f;
        [SerializeField] private float _wallRunTweenDuration = 0.25f;

        private Camera _cameraComponent;
        private float _defaultFov;
        private float _currentPitch;
        private float _currentTiltZ;
        private float _targetTiltZ;
        private float _targetFov;
        private bool _isWallRunActive;
        private float _tiltSign;
        private bool _isLocalCameraActive = true;
#if DOTWEEN_ENABLED
        private Tween _fovTween;
        private Tween _tiltTween;
#endif

        public void SetUseMainCamera(bool useMainCamera)
        {
            _isLocalCameraActive = useMainCamera;
            if (_localOnlyObjects == null)
            {
                return;
            }
            for (int i = 0; i < _localOnlyObjects.Length; i++)
            {
                GameObject localOnlyObject = _localOnlyObjects[i];
                if (localOnlyObject != null)
                {
                    localOnlyObject.SetActive(_isLocalCameraActive);
                }
            }
        }

        public void ApplyLook(CameraLookCommand command)
        {
            if (_bodyTransform != null)
            {
                _bodyTransform.rotation = Quaternion.Euler(0f, command.Yaw, 0f);
            }

            if (!_isLocalCameraActive)
            {
                return;
            }

            _currentPitch = command.Pitch;
            if (_pitchPivot != null)
            {
                _pitchPivot.localRotation = Quaternion.Euler(_currentPitch, 0f, _currentTiltZ);
            }
            else if (_cameraTransform != null)
            {
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

            if (!_isLocalCameraActive)
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

            if (_pitchPivot == null)
            {
                _pitchPivot = _cameraTransform;
            }

            if (_cameraTransform != null)
            {
                _cameraComponent = _cameraTransform.GetComponent<Camera>();
                _currentPitch = _cameraTransform.localEulerAngles.x;
            }

            if (_cameraComponent != null)
            {
                _defaultFov = _cameraComponent.fieldOfView;
                _targetFov = _defaultFov;
            }
            _targetTiltZ = _currentTiltZ;
        }

        private void AnimateCamera(float targetFov, float targetTilt)
        {
            _targetFov = targetFov;
            _targetTiltZ = targetTilt;

            if (_cameraComponent != null)
            {
#if DOTWEEN_ENABLED
                _fovTween?.Kill();
                _fovTween = _cameraComponent.DOFieldOfView(targetFov, _wallRunTweenDuration);
#else
                if (_wallRunTweenDuration <= 0f)
                {
                    _cameraComponent.fieldOfView = targetFov;
                }
#endif
            }

#if DOTWEEN_ENABLED
            _tiltTween?.Kill();
            _tiltTween = DOTween.To(
                () => _currentTiltZ,
                value =>
                {
                    _currentTiltZ = value;
                    if (_pitchPivot != null)
                    {
                        _pitchPivot.localRotation = Quaternion.Euler(_currentPitch, 0f, _currentTiltZ);
                    }
                    else if (_cameraTransform != null)
                    {
                        _cameraTransform.localRotation = Quaternion.Euler(_currentPitch, 0f, _currentTiltZ);
                    }
                },
                targetTilt,
                _wallRunTweenDuration);
#else
            if (_wallRunTweenDuration <= 0f)
            {
                _currentTiltZ = targetTilt;
                if (_pitchPivot != null)
                {
                    _pitchPivot.localRotation = Quaternion.Euler(_currentPitch, 0f, _currentTiltZ);
                }
                else if (_cameraTransform != null)
                {
                    _cameraTransform.localRotation = Quaternion.Euler(_currentPitch, 0f, _currentTiltZ);
                }
            }
#endif
        }

        private void Update()
        {
#if !DOTWEEN_ENABLED
            float duration = Mathf.Max(0.0001f, _wallRunTweenDuration);
            float t = Time.deltaTime / duration;

            if (_cameraComponent != null)
            {
                _cameraComponent.fieldOfView = Mathf.Lerp(_cameraComponent.fieldOfView, _targetFov, t);
            }

            _currentTiltZ = Mathf.Lerp(_currentTiltZ, _targetTiltZ, t);
            if (_pitchPivot != null)
            {
                _pitchPivot.localRotation = Quaternion.Euler(_currentPitch, 0f, _currentTiltZ);
            }
            else if (_cameraTransform != null)
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
