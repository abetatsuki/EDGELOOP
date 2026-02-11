using UnityEngine;

namespace Runtime
{
    public class CameraLook : ILookInputPort
    {
        private readonly ICameraRotationOutput _cameraRotationOutput;
        private readonly CameraConfigData _cameraConfigData;
        private float _pitch;

        public CameraLook(ICameraRotationOutput cameraRotationOutput, CameraConfigData cameraConfigData)
        {
            _cameraRotationOutput = cameraRotationOutput;
            _cameraConfigData = cameraConfigData;
        }

        public void Handle(LookInputData data)
        {
            float x = data.X * _cameraConfigData.LookSpeed;
            float y = data.Y * _cameraConfigData.LookSpeed;

            _pitch -= y;
            _pitch = Mathf.Clamp(_pitch, -_cameraConfigData.MaxPitch, _cameraConfigData.MaxPitch);

            _cameraRotationOutput.ApplyLook(new CameraLookCommand(x, _pitch));
        }
    }
}
