using Develop.Data;
using Develop.Interface;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Develop.UI
{
    [RequireComponent(typeof(PlayerInput))]
    ///<summary>
    /// 入力をバッファするクラス。
    ///</summary>
    public class InputBuffer : MonoBehaviour
    {
        public void Init(IPlayerInputPort inputPort)
        {
            _inputPort = inputPort;
        }
        public void OnMove(InputAction.CallbackContext context)
        {
            Vector2 input = context.ReadValue<Vector2>();
            _inputPort?.OnMoveInput(input);
        }

        private IPlayerInputPort _inputPort;


    }
}
