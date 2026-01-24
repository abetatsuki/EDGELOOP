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
        private const string SPRINT = "Sprint";

        private bool _isMoveActive;
        private InputEventHandler _moveHandler;
        private InputEventHandler _sprintHandler;

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

            _moveHandler.OnPerformed += OnMove;

            _moveHandler.OnCanceled += OnMove;

            _handlers.Add(_moveHandler);

            _sprintHandler = new InputEventHandler(SPRINT);

            _sprintHandler.OnPerformed 

            
        }

        private void OnMove(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _isMoveActive = true;
            }
            else if (context.canceled)
            {
                _isMoveActive= false;
            }
   
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
