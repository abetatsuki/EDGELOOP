using UnityEngine;
namespace Runtime
{
    public class RigidBodyAdaptor : MonoBehaviour, IJumpPhysicsOutput
    {
        Rigidbody _rigidbody;
        public void ApplyJump(JumpCommand command)
        {
            _rigidbody.AddForce(Vector3.up * command.Power, ForceMode.Impulse);
         }
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }
    }
}