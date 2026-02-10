using UnityEngine;
namespace Runtime
{
    public class RigidBodyAdaptor : MonoBehaviour, IJumpPhysicsOutput, IMovePhysicsOutput
    {
        private Rigidbody _rigidbody;
        public void ApplyJump(JumpCommand command)
        {
            _rigidbody.AddForce(Vector3.up * command.Power, ForceMode.Impulse);
         }

        public void ApplyMove(MoveCommand command)
        {
            Vector3 currentVelocity = _rigidbody.linearVelocity;
            Vector3 moveVelocity = new Vector3(command.X, 0f, command.Y) * command.Speed;
            _rigidbody.linearVelocity = new Vector3(moveVelocity.x, currentVelocity.y, moveVelocity.z);
        }
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }
    }
}
