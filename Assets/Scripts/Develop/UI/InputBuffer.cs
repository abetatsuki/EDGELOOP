using UnityEngine;
using UnityEngine.InputSystem;

namespace UI
{
    [RequireComponent(typeof(PlayerInput))]
    ///<summary>
    /// 入力をバッファするクラス。
    ///</summary>
    public class InputBuffer : MonoBehaviour
    {


        public void OnMove(InputAction.CallbackContext context)
        {
            Vector2 input = context.ReadValue<Vector2>();
            Vector3 value = new Vector3(input.y, 0, input.x);
        }

        
    }
}
