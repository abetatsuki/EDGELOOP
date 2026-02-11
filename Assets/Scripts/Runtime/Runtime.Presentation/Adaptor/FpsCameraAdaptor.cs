using UnityEngine;

namespace Runtime
{
    public class FpsCameraAdaptor : MonoBehaviour, ICameraRotationOutput
    {
        [SerializeField] private Transform _bodyTransform;
        [SerializeField] private Transform _cameraTransform;

        public void ApplyLook(CameraLookCommand command)
        {
            if (_bodyTransform != null)
            {
                _bodyTransform.rotation = Quaternion.Euler(0f, command.Yaw, 0f);
            }

            if (_cameraTransform != null)
            {
                _cameraTransform.localRotation = Quaternion.Euler(command.Pitch, 0f, 0f);
            }
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
        }
    }
}
