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

        public void OnMove(InputAction.CallbackContext context)
        {
            Vector2 value = context.ReadValue<Vector2>();
            _movePort.Handle(new MoveInputData { X = value.x, Y = value.y });
        }

        private PlayerInput _playerInput;

        private const string JUMP_ACTION = "Jump";
        private const string MOVE_ACTION = "Move";
        private InputAction _jumpAction;
        private InputAction _moveAction;

        private void OnEnable()
        {
            PlayerInputSetUp();
        }

        private void OnDisable()
        {
            if (_jumpAction != null)
            {
                _jumpAction.performed -= OnJump;
                _jumpAction.canceled -= OnJump;
            }
            if (_moveAction != null)
            {
                _moveAction.performed -= OnMove;
                _moveAction.canceled -= OnMove;
            }
        }

        private void PlayerInputSetUp()
        {
            _playerInput = GetComponent<PlayerInput>();
            _jumpAction = _playerInput.actions[JUMP_ACTION];
            _jumpAction.performed += OnJump;
            _jumpAction.canceled += OnJump;

            _moveAction = _playerInput.actions[MOVE_ACTION];
            _moveAction.performed += OnMove;
            _moveAction.canceled += OnMove;
        }
    }
}
