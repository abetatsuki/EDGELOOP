using UnityEngine;
namespace Runtime
{
    public class RigidBodyAdaptor : MonoBehaviour, IJumpPhysicsOutput, IMovePhysicsOutput, IDashPhysicsOutput, ISlideMotionOutput
    {
        [SerializeField] private float _slideImpulseScale = 1f;
        [SerializeField] private float _crouchScaleY = 0.6f;
        private Rigidbody _rigidbody;
        private Vector3 _defaultScale;
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

        public void ApplyDash(DashCommand command)
        {
            Vector3 dashVelocity = new Vector3(command.X, 0f, command.Y) * command.Power;
            _rigidbody.AddForce(dashVelocity, ForceMode.Impulse);
        }

        public void ApplySlideMotion(SlideMotionCommand command)
        {
            if (command.Mode == SlideMode.Slide)
            {
                Vector3 slideVelocity = new Vector3(command.X, 0f, command.Y) * (command.Power * _slideImpulseScale);
                _rigidbody.AddForce(slideVelocity, ForceMode.Impulse);
                SetCrouchScale(true);
                return;
            }

            if (command.Mode == SlideMode.Crouch)
            {
                SetCrouchScale(true);
                return;
            }

            SetCrouchScale(false);
        }

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _defaultScale = transform.localScale;
        }

        private void SetCrouchScale(bool isCrouching)
        {
            if (isCrouching)
            {
                transform.localScale = new Vector3(_defaultScale.x, _defaultScale.y * _crouchScaleY, _defaultScale.z);
                return;
            }
            transform.localScale = _defaultScale;
        }
    }
}
