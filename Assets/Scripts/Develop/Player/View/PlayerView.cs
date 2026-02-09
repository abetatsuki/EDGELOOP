using Develop.Interface;
using Develop.UI;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Cinemachine; // Unity.Cinemachine から Cinemachine に変更

namespace Develop.Player.View
{
    [RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))] // CapsuleColliderを必須にする
    public class PlayerView : MonoBehaviour, IMovableBody, IPlayer, IDamageable
    {
        [SerializeField]
        private CinemachineImpulseSource _impulseSource;
        public CinemachineImpulseSource ImpulseSource => _impulseSource;

        [SerializeField]
        private Transform _attackTf;
        public Transform AttackTransform => _attackTf;

        private CapsuleCollider _capsuleCollider; // 追加
        private float _defaultColliderHeight; // 追加
        private float _defaultColliderCenterY; // 追加

        // IMovableBodyプロパティの実装
        public float ColliderHeight
        {
            get => _capsuleCollider.height;
            set => _capsuleCollider.height = value;
        }
        public float ColliderCenterY
        {
            get => _capsuleCollider.center.y;
            set
            {
                Vector3 c = _capsuleCollider.center; // コピーを作る
                c.y = value;                          // コピーを変更
                _capsuleCollider.center = c;          // プロパティに代入
            }
        }
        public float DefaultColliderHeight => _defaultColliderHeight; // 追加
        public float DefaultColliderCenterY => _defaultColliderCenterY; // 追加


        public void Init(PlayerPresenter presenter, InputBuffer buffer)
        {
            _playerUpdate = presenter;
            _inputBuffer = buffer;

            _inputBuffer.Awake();

            _playerUpdate.OnHealthChanged += UpdateHealthUI;
            // スライディング状態変化の購読
            _playerUpdate.PlayerEntity.OnSlidingStatusChanged += OnSlideStatusChanged;
            UpdateHealthUI(_playerUpdate.CurrentHealth);
        }

        private void Awake() // Awakeメソッドの追加または修正
        {
            _capsuleCollider = GetComponent<CapsuleCollider>();
            _defaultColliderHeight = _capsuleCollider.height;
            _defaultColliderCenterY = _capsuleCollider.center.y;
        }

        private void OnDestroy()
        {
            if (_playerUpdate != null)
            {
                _playerUpdate.OnHealthChanged -= UpdateHealthUI;
                // スライディング状態変化の購読解除
                _playerUpdate.PlayerEntity.OnSlidingStatusChanged -= OnSlideStatusChanged;
            }
        }

        private void UpdateHealthUI(int newHealth)
        {
            Debug.Log($"PlayerView: Health updated to {newHealth}");
        }

        private void OnSlideStatusChanged(bool isSliding) // 追加
        {
            if (isSliding)
            {
                // スライディング時のコライダー設定
                ColliderHeight = _playerUpdate.SlideColliderHeight;
                ColliderCenterY = _playerUpdate.SlideColliderCenterY;
            }
            else
            {
                // デフォルトのコライダー設定に戻す
                ColliderHeight = DefaultColliderHeight;
                ColliderCenterY = DefaultColliderCenterY;
            }
        }

        public Vector3 Position
        {
            get => Rb.position;
            set => Rb.position = value;
        }
        public Vector3 Velocity
        {
            get => Rb.linearVelocity;
            set => Rb.linearVelocity = value;
        }
        public Quaternion PlayerQuaternion
        {
            get => Tf.rotation;
            set => Tf.rotation = value;
        }
        public Quaternion CameraQuaternion
        {
            get => _camera.localRotation;
            set => _camera.localRotation = value;
        }

        public Vector3 Forward
        {
            get => _camera.forward;
        }

        public float LinearDamping
        {
            get => Rb.linearDamping;
            set => Rb.linearDamping = value;
        }
        public Rigidbody Rigidbody => Rb;

        public void TakeDamage(int damage, Vector3 attackDirection)
        {
            _playerUpdate.TakeDamage(damage, attackDirection);
        }

        [SerializeField]
        private Transform _camera;
        public PlayerInput PlayerInput => _playerInput;
        private PlayerPresenter _playerUpdate;
        private InputBuffer _inputBuffer;
        private PlayerInput _playerInput => _pi ??= GetComponent<PlayerInput>();
        private PlayerInput _pi;
        private Transform Tf => _tf ??= GetComponent<Transform>();
        private Transform _tf;
        private Rigidbody Rb => _rb ??= GetComponent<Rigidbody>();
        private Rigidbody _rb;

        private void Update()
        {
            _inputBuffer.Update();
            _playerUpdate.Update();
        }
    }
}
