using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;

namespace Runtime
{
    [RequireComponent(typeof(PlayerInput))]
    public class InputBuffer : MonoBehaviour
    {
        [Inject] private IJumpInputPort _jumpPort;

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

        private PlayerInput _playerInput;

        private const string JUMP_ACTION = "Jump";
        private InputAction _jumpAction;
        private void PlayerInputSetUp()
        {
            _playerInput = GetComponent<PlayerInput>();
            _jumpAction = _playerInput.actions[JUMP_ACTION];
            _jumpAction.performed += OnJump;
            _jumpAction.canceled += OnJump;
        }
    }
}
