using UnityEngine;

namespace Runtime
{
    public class CameraLook : ILookInputPort
    {
        private readonly ICameraRotationOutput _cameraRotationOutput;
        private readonly CameraConfigData _cameraConfigData;
        private readonly IControlRotationReader _controlRotationReader;
        private readonly IControlRotationWriter _controlRotationWriter;

        public CameraLook(
            ICameraRotationOutput cameraRotationOutput,
            CameraConfigData cameraConfigData,
            IControlRotationReader controlRotationReader,
            IControlRotationWriter controlRotationWriter)
        {
            _cameraRotationOutput = cameraRotationOutput;
            _cameraConfigData = cameraConfigData;
            _controlRotationReader = controlRotationReader;
            _controlRotationWriter = controlRotationWriter;
        }

        public void Handle(LookInputData data)
        {
            ControlRotationData current = _controlRotationReader.GetControlRotation();

            float yaw = current.Yaw + (data.X * _cameraConfigData.LookSpeed);
            float pitch = current.Pitch - (data.Y * _cameraConfigData.LookSpeed);
            pitch = Mathf.Clamp(pitch, -_cameraConfigData.MaxPitch, _cameraConfigData.MaxPitch);

            ControlRotationData updated = new ControlRotationData(yaw, pitch);
            _controlRotationWriter.SetControlRotation(updated);
            _cameraRotationOutput.ApplyLook(new CameraLookCommand(updated.Yaw, updated.Pitch));
        }
    }
}
