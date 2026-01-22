using System;
using UnityEngine.InputSystem;

namespace Develop.UI
{
    /// <summary>
    /// InputAction の各イベントフェーズへの登録・解除を管理するクラス
    /// </summary>
    public class InputEventHandler
    {
        public InputEventHandler(string actionName)
        {
            _actionName = actionName;
        }

        public event Action<InputAction.CallbackContext> OnStarted;
        public event Action<InputAction.CallbackContext> OnPerformed;
        public event Action<InputAction.CallbackContext> OnCanceled;

        public InputAction Action;
        public void Bind(PlayerInput playerInput)
        {
            if (playerInput == null) return;
            Action = playerInput.actions[_actionName];
            if (Action == null) return;

            if (OnStarted != null) Action.started += OnStarted;
            if (OnPerformed != null) Action.performed += OnPerformed;
            if (OnCanceled != null) Action.canceled += OnCanceled;
        }

        public void Unbind()
        {
            if (Action == null) return;

            if (OnStarted != null) Action.started -= OnStarted;
            if (OnPerformed != null) Action.performed -= OnPerformed;
            if (OnCanceled != null) Action.canceled -= OnCanceled;
        }

        private readonly string _actionName;
      
    }
}
