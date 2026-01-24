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
        private const string SLIDE = "Slide";

        private bool _isMoveActive;
        private InputAction _moveAction;
        private InputAction _sprintAction;
        private InputAction _slideAction;
        

        private void Awake()
        {
            _playerInput = GetComponent<PlayerInput>();

            CreateMoveEvent();
        }

        private void Update()
        {
            if (!_isMoveActive ) return;

           // Vector2 input = _moveHandler.Action.ReadValue<Vector2>();
           Vector2 input = _moveAction.ReadValue<Vector2>();

            _playerInputPort?.OnMoveInput(input, Time.deltaTime);
        }

        private void CreateMoveEvent()
        {
            _moveAction = _playerInput.actions[MOVE];
            _sprintAction = _playerInput.actions[SPRINT];
            _slideAction = _playerInput.actions[SLIDE];

            _moveAction.performed += OnMove;
            _moveAction.canceled += OnMove;

            _sprintAction.performed += OnSprint;
            _sprintAction.canceled += OnSprint;

            _slideAction.performed += OnSlide;
            _slideAction.canceled += OnSlide;

   
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
        private void OnSprint(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _playerInputPort.OnRunInput(true);
            }
            else if (context.canceled)
            {
                _playerInputPort.OnRunInput(false);
            }
        }

        private void OnSlide(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _playerInputPort.OnSlideInput(true);
            }
            else if (context.canceled)
            {
                _playerInputPort.OnSlideInput(false);
            }
        }

        private void OnDestroy()
        {
        }
    }
}
