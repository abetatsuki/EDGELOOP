using Develop.Data;
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
        public void Init(DataContainer dataContainer)
        {
            _dataContainer = dataContainer;
        }
        public void OnMove(InputAction.CallbackContext context)
        {
            Vector2 input = context.ReadValue<Vector2>();
            Vector3 value = new Vector3(input.y, 0, input.x);
            _dataContainer.PlayerPresenter.OnMove(value);
        }

        private DataContainer _dataContainer;


    }
}
