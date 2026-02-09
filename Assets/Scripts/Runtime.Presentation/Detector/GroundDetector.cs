using UnityEngine;
namespace Runtime
{
    public class GroundDetector : MonoBehaviour
    {
        public bool IsGround { get; private set; }

        private void OnCollisionEnter(Collision collision)
        {
            IsGround = true;
        }
        private void OnCollisionExit(Collision collision)
        {
            IsGround = false;
        }
    }
}