using UnityEngine;
using UnityEngine.InputSystem;

namespace Runtime
{
    [RequireComponent(typeof(PlayerInput))]
    public class InputBuffer : MonoBehaviour
    {
        [Header("Standalone Cursor")]
        [SerializeField] private bool _lockCursorOnEnable = true;

        private PlayerInput _playerInput;

        private const string JUMP_ACTION = "Jump";
        private const string MOVE_ACTION = "Move";
        private const string DASH_ACTION = "Sprint";
        private const string SLIDE_ACTION = "Slide";
        private const string LOOK_ACTION = "Look";

        private InputAction _jumpAction;
        private InputAction _moveAction;
        private InputAction _dashAction;
        private InputAction _slideAction;
        private InputAction _lookAction;

        private Vector2 _move;
        private Vector2 _look;
        private bool _jumpHeld;
        private bool _dashHeld;
        private bool _slideHeld;
        private bool _jumpPressed;
        private bool _dashPressed;
        private bool _slidePressed;
        private sbyte _runSpeedDelta;
        private bool _climbHeld;
        private bool _descendHeld;

        public void OnJump(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _jumpHeld = true;
                _jumpPressed = true;
            }
            else if (context.canceled)
            {
                _jumpHeld = false;
            }
        }

        public void OnDash(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _dashHeld = true;
                _dashPressed = true;
            }
            else if (context.canceled)
            {
                _dashHeld = false;
            }
        }

        public void OnSlide(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _slideHeld = true;
                _slidePressed = true;
            }
            else if (context.canceled)
            {
                _slideHeld = false;
            }
        }

        private void OnEnable()
        {
            PlayerInputSetUp();
            if (_lockCursorOnEnable)
            {
                ApplyCursorLock(true);
            }
        }

        private void Update()
        {
            if (_moveAction != null)
            {
                _move = _moveAction.ReadValue<Vector2>();
            }

            if (_lookAction != null)
            {
                // Mouse delta is frame-based. Accumulate until network tick consumes it.
                _look += _lookAction.ReadValue<Vector2>();
            }

            if (Mouse.current != null)
            {
                float scrollY = Mouse.current.scroll.ReadValue().y;
                if (Mathf.Abs(scrollY) > 0.001f)
                {
                    _runSpeedDelta = (sbyte)Mathf.RoundToInt(Mathf.Sign(scrollY));
                }
            }

            if (Keyboard.current != null)
            {
                _climbHeld = Keyboard.current.leftShiftKey.isPressed;
                _descendHeld = Keyboard.current.leftCtrlKey.isPressed;
            }
            else
            {
                _climbHeld = false;
                _descendHeld = false;
            }
        }

        public NetInput BuildNetInputAndConsumeOneShots()
        {
            NetInput netInput = new NetInput
            {
                Move = _move,
                Look = _look,
                JumpHeld = _jumpHeld,
                DashHeld = _dashHeld,
                SlideHeld = _slideHeld,
                JumpPressed = _jumpPressed,
                DashPressed = _dashPressed,
                SlidePressed = _slidePressed,
                RunSpeedDelta = _runSpeedDelta,
                ClimbHeld = _climbHeld,
                DescendHeld = _descendHeld,
            };

            _jumpPressed = false;
            _dashPressed = false;
            _slidePressed = false;
            _runSpeedDelta = 0;
            _look = Vector2.zero;

            return netInput;
        }

        private void OnDisable()
        {
            if (_jumpAction != null)
            {
                _jumpAction.performed -= OnJump;
                _jumpAction.canceled -= OnJump;
            }

            if (_dashAction != null)
            {
                _dashAction.performed -= OnDash;
                _dashAction.canceled -= OnDash;
            }

            if (_slideAction != null)
            {
                _slideAction.performed -= OnSlide;
                _slideAction.canceled -= OnSlide;
            }

            if (_lockCursorOnEnable)
            {
                ApplyCursorLock(false);
            }
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            if (!_lockCursorOnEnable)
            {
                return;
            }

            ApplyCursorLock(hasFocus);
        }

        private void PlayerInputSetUp()
        {
            _playerInput = GetComponent<PlayerInput>();
            _jumpAction = _playerInput.actions[JUMP_ACTION];
            _jumpAction.performed += OnJump;
            _jumpAction.canceled += OnJump;

            _moveAction = _playerInput.actions[MOVE_ACTION];

            _dashAction = _playerInput.actions[DASH_ACTION];
            _dashAction.performed += OnDash;
            _dashAction.canceled += OnDash;

            _slideAction = _playerInput.actions[SLIDE_ACTION];
            _slideAction.performed += OnSlide;
            _slideAction.canceled += OnSlide;

            _lookAction = _playerInput.actions[LOOK_ACTION];
        }

        private static void ApplyCursorLock(bool lockCursor)
        {
            Cursor.lockState = lockCursor ? CursorLockMode.Locked : CursorLockMode.None;
            Cursor.visible = !lockCursor;
        }
    }
}
