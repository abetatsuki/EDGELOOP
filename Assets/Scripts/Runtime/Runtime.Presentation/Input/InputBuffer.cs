using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;

namespace Runtime
{
    [RequireComponent(typeof(PlayerInput))]
    public class InputBuffer : MonoBehaviour
    {
        [Inject] private IJumpInputPort _jumpPort;
        [Inject] private IMoveInputPort _movePort;
        [Inject] private IDashInputPort _dashPort;

        public void OnJump(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _jumpPort.Handle(new JumpInputData { IsPressed = true });
            }
            else if (context.canceled)
            {
                _jumpPort.Handle(new JumpInputData { IsPressed = false });
            }
        }

        public void OnDash(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _dashPort.Handle(new DashInputData { IsPressed = true });
            }
            else if (context.canceled)
            {
                _dashPort.Handle(new DashInputData { IsPressed = false });
            }
        }

        private PlayerInput _playerInput;

        private const string JUMP_ACTION = "Jump";
        private const string MOVE_ACTION = "Move";
        private const string DASH_ACTION = "Sprint";
        private InputAction _jumpAction;
        private InputAction _moveAction;
        private InputAction _dashAction;

        private void OnEnable()
        {
            PlayerInputSetUp();
        }

        private void Update()
        {
            if (_moveAction == null)
            {
                return;
            }
            Vector2 value = _moveAction.ReadValue<Vector2>();
            _movePort.Handle(new MoveInputData { X = value.x, Y = value.y });
        }

        private void OnDisable()
        {
            if (_jumpAction != null)
            {
                _jumpAction.performed -= OnJump;
                _jumpAction.canceled -= OnJump;
            }

            if (_dashAction != null)
            {
                _dashAction.performed -= OnDash;
                _dashAction.canceled -= OnDash;
            }
        }

        private void PlayerInputSetUp()
        {
            _playerInput = GetComponent<PlayerInput>();
            _jumpAction = _playerInput.actions[JUMP_ACTION];
            _jumpAction.performed += OnJump;
            _jumpAction.canceled += OnJump;

            _moveAction = _playerInput.actions[MOVE_ACTION];

            _dashAction = _playerInput.actions[DASH_ACTION];
            _dashAction.performed += OnDash;
            _dashAction.canceled += OnDash;
        }
    }
}
