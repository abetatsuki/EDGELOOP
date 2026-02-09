using UnityEngine;
using UnityEngine.InputSystem;
namespace Runtime
{
    [RequireComponent(typeof(PlayerInput))]
    public class InputBuffer : MonoBehaviour
    {
        IJumpInputPort _jumpPort;

        public void OnJump(InputAction.CallbackContext context)
        {
            if(context.performed)
            {
                _jumpPort.Handle(new JumpInputData { IsPressed = true });
            }
            else if(context.canceled)
            {
                _jumpPort.Handle(new JumpInputData { IsPressed = false });
            }
        }
    }
}
