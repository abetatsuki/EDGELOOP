using UnityEngine;
namespace Runtime
{
    public class JumpCommand
    {
        public JumpCommand(float power)
        {
            Power = power;
        }
        public float Power { get; private set; }
    }

}
