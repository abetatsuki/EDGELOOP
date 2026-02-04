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

        public Quaternion CalculateSway(Vector2 lookInput, Quaternion currentSway, float deltaTime)
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

            // The target sway rotation based on input
            var targetSway = Quaternion.Euler(swayX, swayY, 0f);

            // Smoothly interpolate from the current sway to the target sway.
            // When there is no input, targetSway will be Quaternion.identity,
            // so the gun will smoothly return to the center.
            var newSway = Quaternion.Slerp(
                currentSway,
                targetSway,
                deltaTime * _swaySmooth
            );
            
            return newSway;
        }
    }
}
