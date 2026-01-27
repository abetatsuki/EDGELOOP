using Develop.Interface;
using Develop.UI;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Develop.Player.View
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerView : MonoBehaviour, IMovableBody,IPlayer
    {
        public void Init(PlayerPresenter presetner , InputBuffer buffer)
        {
            _playerUpdate = presetner;
            _inputBuffer = buffer;
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

        public float LinearDamping
        {
            get => Rb.linearDamping;
            set => Rb.linearDamping = value;
        }

        


        [SerializeField]
        private Transform _camera;
        public PlayerInput PlayerInput => _playerInput;
        private PlayerPresenter _playerUpdate;
        private InputBuffer _inputBuffer;
        private PlayerInput _playerInput => _pi??=GetComponent<PlayerInput>();
        private PlayerInput _pi;

        private Transform Tf => _tf??= GetComponent<Transform>();
        private Transform _tf;
        private Rigidbody Rb => _rb ??= GetComponent<Rigidbody>();
        private Rigidbody _rb;

        private void Awake()
        {
            _inputBuffer.Awake();
        }
        private void Update()
        {
            _inputBuffer.Update();
            _playerUpdate.Update();
        }
    }
}