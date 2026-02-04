using Develop.Gun;
using Develop.Interface;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Develop.UI
{
    [RequireComponent(typeof(PlayerInput))]
    /// <summary>
    /// 入力をバッファし、ポーリング用の状態を管理するクラス
    /// </summary>
    public class InputBuffer : IPlayerUpdatable, IAwakeable
    {
        public InputBuffer(IPlayer player, IPlayerInputPort presenter,IGunRequest request)
        {
            _playerInput = player.PlayerInput;
            _playerInputPort = presenter;
            _gunRequest = request;
        }
        public void Awake()
        {
            CreateMoveEvent();
        }

        public void Update()
        {
            if (!_isMoveActive) return;

            // Vector2 input = _moveHandler.Action.ReadValue<Vector2>();
            Vector2 input = _moveAction.ReadValue<Vector2>();

            _playerInputPort?.OnMoveInput(input, Time.deltaTime);
        }

        private IPlayerInputPort _playerInputPort;
        private IGunRequest _gunRequest;
        private PlayerInput _playerInput;

        private const string MOVE = "Move";
        private const string SPRINT = "Sprint";
        private const string SLIDE = "Slide";
        private const string LOOK = "Look";
        private const string ATTACK = "Attack";
        private const string AIM = "Aim";
        private const string RELOAD = "Reload";

        private bool _isMoveActive;
        private InputAction _moveAction;
        private InputAction _sprintAction;
        private InputAction _slideAction;
        private InputAction _lookAction;
        private InputAction _attackAction;
        private InputAction _aimAction;
        private InputAction _reloadAction;


        private void CreateMoveEvent()
        {
            _moveAction = _playerInput.actions[MOVE];
            _sprintAction = _playerInput.actions[SPRINT];
            _slideAction = _playerInput.actions[SLIDE];
            _lookAction = _playerInput.actions[LOOK];
            _attackAction = _playerInput.actions[ATTACK];
            _aimAction = _playerInput.actions[AIM];
            _reloadAction = _playerInput.actions[RELOAD];

            _moveAction.performed += OnMove;
            _moveAction.canceled += OnMove;

            _sprintAction.performed += OnSprint;
            _sprintAction.canceled += OnSprint;

            _slideAction.performed += OnSlide;
            _slideAction.canceled += OnSlide;

            _lookAction.performed += OnLook;
            _lookAction.canceled += OnLook;

            _attackAction.performed += OnAttack;
            _attackAction.canceled += OnAttack;

            _aimAction.performed += OnAim;
            _aimAction.canceled += OnAim;

            _reloadAction.performed += OnReload;
        }

        private void OnMove(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _isMoveActive = true;
            }
            else if (context.canceled)
            {
                _isMoveActive = false;
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

        private void OnLook(InputAction.CallbackContext context)
        {
            Vector2 input = context.ReadValue<Vector2>();
             _playerInputPort.OnLookInput(input);
            _gunRequest.OnLookInput(input);
        }

        private void OnAttack(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
               _gunRequest.OnFireRequest(true);
            }
            else if (context.canceled)
            {
                _gunRequest.OnFireRequest(false);
            }
        }
        public void OnReload(InputAction.CallbackContext context)
        {
            if (context.performed)
            _gunRequest.OnReloadRequest();
        }
        public void OnAim(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _gunRequest.OnAimRequest(true);
            }
            else if (context.canceled)
            {
                _gunRequest.OnAimRequest(false);
            }
        }
        private void OnDestroy()
        {
        }
    }
}
