using Develop.Interface;
using UnityEngine;

namespace Develop.Player.View
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerView : MonoBehaviour, IMovableBody
    {
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

        public float LinearDamping
        {
            get => Rb.linearDamping;
            set => Rb.linearDamping = value;
        }

        private Rigidbody Rb => _rb ??= GetComponent<Rigidbody>();
        private Rigidbody _rb;
    }
}