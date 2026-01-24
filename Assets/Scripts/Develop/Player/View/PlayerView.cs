using Develop.Interface;
using Unity.VisualScripting;
using UnityEngine;

namespace Develop.Player.View
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerView : MonoBehaviour, IMovableBody
    {
        public Vector3 Position
        {
            get => _rb.position;
            set => _rb.position = value;
        }
        public Vector3 Velocity
        {
            get => _rb.linearVelocity;
            set => _rb.linearVelocity = value;
        }

        public float LinearDamping
        {
            get => _rb.linearDamping;
            set => _rb.linearDamping = value;
        }
        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        private Rigidbody _rb;
    }
}