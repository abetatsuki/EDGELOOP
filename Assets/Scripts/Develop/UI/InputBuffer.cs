using Develop.Interface;
using Develop.Player;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

namespace Develop.UI
{
    [RequireComponent(typeof(PlayerInput))]
    /// <summary>
    /// 入力をバッファし、ポーリング用の状態を管理するクラス
    /// </summary>
    public class InputBuffer : MonoBehaviour
    {
        public void Init(IPlayerInputPort presenter)
        {
            _playerInputPort = presenter;
        }

        private IPlayerInputPort _playerInputPort;
        private PlayerInput _playerInput;

        private const string MOVE = "Move";

        private bool _isMoveActive;
        private InputEventHandler _moveHandler;

        private List<InputEventHandler> _handlers = new List<InputEventHandler>();

        private void Awake()
        {
            _playerInput = GetComponent<PlayerInput>();

            CreateMoveEvent();

            EventBind();
        }

        private void Update()
        {
            if (!_isMoveActive ) return;

            Vector2 input = _moveHandler.Action.ReadValue<Vector2>();

            _playerInputPort?.OnMoveInput(input, Time.deltaTime);
        }

        private void CreateMoveEvent()
        {
            _moveHandler = new InputEventHandler(MOVE);

            _moveHandler.OnPerformed += OnMoveStarted;

            _moveHandler.OnCanceled += OnMoveCanceled;

            _handlers.Add(_moveHandler);
        }

        private void OnMoveStarted(InputAction.CallbackContext context)
        {
            if (_isMoveActive == true) return;
            _isMoveActive = true;
        }

        private void OnMoveCanceled(InputAction.CallbackContext context)
        {
            if (_isMoveActive == false) return;
            _isMoveActive = false;

            _playerInputPort?.OnMoveInput(Vector2.zero, Time.deltaTime);
        }

        private void EventBind()
        {
            foreach (var handler in _handlers)
            {
                handler.Bind(_playerInput);
            }
        }

        private void OnDestroy()
        {
            foreach (var handler in _handlers)
            {
                handler.Unbind();
            }

            _handlers.Clear();
        }
    }
}
