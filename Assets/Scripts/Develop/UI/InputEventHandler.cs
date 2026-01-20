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
            // 対象となる InputAction 名を保持する
            _actionName = actionName;
        }

        public event Action<InputAction.CallbackContext> OnStarted;
        public event Action<InputAction.CallbackContext> OnPerformed;
        public event Action<InputAction.CallbackContext> OnCanceled;

        public void Bind(PlayerInput playerInput)
        {
            // PlayerInput から対象の InputAction を取得する
            _action = playerInput.actions[_actionName];
            if (_action == null) return;

            // 各フェーズのイベントを登録する
            if (OnStarted != null) _action.started += OnStarted;
            if (OnPerformed != null) _action.performed += OnPerformed;
            if (OnCanceled != null) _action.canceled += OnCanceled;
        }

        public void Unbind()
        {
            if (_action == null) return;

            // 各フェーズのイベントを解除する
            if (OnStarted != null) _action.started -= OnStarted;
            if (OnPerformed != null) _action.performed -= OnPerformed;
            if (OnCanceled != null) _action.canceled -= OnCanceled;
        }

        private readonly string _actionName;
        private InputAction _action;
    }
}
