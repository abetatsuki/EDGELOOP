using UnityEngine;
namespace Runtime
{
    public class JumpCommand
    {
        public JumpCommand(float power)
        {
            Power = power;
            HorizontalDirection = Vector3.zero;
            HorizontalPower = 0f;
        }

        public JumpCommand(float power, Vector3 horizontalDirection, float horizontalPower)
        {
            Power = power;
            HorizontalDirection = horizontalDirection;
            HorizontalPower = horizontalPower;
        }
        public float Power { get; private set; }
        public Vector3 HorizontalDirection { get; private set; }
        public float HorizontalPower { get; private set; }
    }

}
