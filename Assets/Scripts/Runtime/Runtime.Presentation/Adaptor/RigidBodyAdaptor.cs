using UnityEngine;
namespace Runtime
{
    public class RigidBodyAdaptor : MonoBehaviour, IJumpPhysicsOutput, IMovePhysicsOutput, IDashPhysicsOutput, ISlideMotionOutput, IWallRunPhysicsOutput
    {
        [SerializeField] private float _slideImpulseScale = 1f;
        [SerializeField] private float _crouchScaleY = 0.6f;
        [SerializeField] private float _wallRunRollDegrees = 20f;
        private Rigidbody _rigidbody;
        private Vector3 _defaultScale;
        private bool _defaultUseGravity;
        private bool _isWallRunning;
        private float _wallRunEndTime;
        private float _currentWallRoll;
        public void ApplyJump(JumpCommand command)
        {
            _rigidbody.AddForce(Vector3.up * command.UpPower, ForceMode.Impulse);
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
            _defaultUseGravity = _rigidbody.useGravity;
        }

        private void Update()
        {
            if (_isWallRunning && Time.time >= _wallRunEndTime)
            {
                EndWallRun();
            }
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

        public void BeginWallRun(WallRunCommand command)
        {
            _isWallRunning = true;
            _wallRunEndTime = Time.time + command.Duration;
            _currentWallRoll = command.Side == WallSide.Left ? _wallRunRollDegrees : -_wallRunRollDegrees;

            _rigidbody.useGravity = false;

            Vector3 forward = transform.forward;
            forward.y = 0f;
            if (forward.sqrMagnitude > 0.0001f)
            {
                forward.Normalize();
                _rigidbody.linearVelocity = forward * command.Speed;
            }

            Vector3 euler = transform.rotation.eulerAngles;
            transform.rotation = Quaternion.Euler(euler.x, euler.y, _currentWallRoll);
        }

        public void EndWallRun()
        {
            if (!_isWallRunning) return;
            _isWallRunning = false;
            _rigidbody.useGravity = _defaultUseGravity;
            Vector3 euler = transform.rotation.eulerAngles;
            transform.rotation = Quaternion.Euler(euler.x, euler.y, 0f);
        }
    }
}
