using UnityEngine;
using System.Collections.Generic;

namespace Runtime
{
    public class CameraLook : ILookInputPort
    {
        private readonly IReadOnlyList<ICameraRotationOutput> _cameraRotationOutputs;
        private readonly IReadOnlyList<IControlRotationOutput> _controlRotationOutputs;
        private readonly CameraConfigData _cameraConfigData;
        private float _yaw;
        private float _pitch;

        public CameraLook(
            IReadOnlyList<ICameraRotationOutput> cameraRotationOutputs,
            CameraConfigData cameraConfigData,
            IReadOnlyList<IControlRotationOutput> controlRotationOutputs)
        {
            _cameraRotationOutputs = cameraRotationOutputs;
            _cameraConfigData = cameraConfigData;
            _controlRotationOutputs = controlRotationOutputs;
        }

        public void Handle(LookInputData data)
        {
            _yaw += data.X * _cameraConfigData.LookSpeed;
            _pitch -= data.Y * _cameraConfigData.LookSpeed;
            _pitch = Mathf.Clamp(_pitch, -_cameraConfigData.MaxPitch, _cameraConfigData.MaxPitch);

            ControlRotationData rotationData = new ControlRotationData(_yaw, _pitch);
            for (int i = 0; i < _controlRotationOutputs.Count; i++)
            {
                _controlRotationOutputs[i].Publish(rotationData);
            }

            CameraLookCommand lookCommand = new CameraLookCommand(rotationData.Yaw, rotationData.Pitch);
            for (int i = 0; i < _cameraRotationOutputs.Count; i++)
            {
                _cameraRotationOutputs[i].ApplyLook(lookCommand);
            }
        }
    }
}
