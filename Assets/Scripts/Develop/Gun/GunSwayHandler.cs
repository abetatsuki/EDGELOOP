using UnityEngine;

namespace Develop.Gun
{
    public class GunSwayHandler
    {
        private readonly float _swayAmount;
        private readonly float _swaySmooth;
        private readonly float _maxSway;

        public GunSwayHandler(GunConfig gunConfig)
        {
            _swayAmount = gunConfig.SwayAmount;
            _swaySmooth = gunConfig.SwaySmooth;
            _maxSway = gunConfig.MaxSway;
        }

        public Quaternion CalculateSway(Vector2 lookInput, Quaternion initialRotation, Quaternion currentRotation, float deltaTime)
        {
            float swayX = Mathf.Clamp(
                -lookInput.y * _swayAmount,
                -_maxSway,
                _maxSway
            );

            float swayY = Mathf.Clamp(
                lookInput.x * _swayAmount,
                -_maxSway,
                _maxSway
            );

            var targetRotation = Quaternion.Euler(swayX, swayY, 0f);

            var newRotation = Quaternion.Slerp(
                currentRotation,
                initialRotation * targetRotation,
                deltaTime * _swaySmooth
            );
            Debug.Log(newRotation);
            return newRotation;
        }
    }
}
