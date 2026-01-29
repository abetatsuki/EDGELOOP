using Develop.Gun.Interface;
using UnityEngine;
using static UnityEngine.UI.Image;
namespace Develop.Gun
{
    public class GunView : MonoBehaviour, IGunView
    {
        public Vector3 Position
        {
            get => _tryTf.localPosition;
            set => _tryTf.localPosition = value;
        }
        public Vector3 FirePosition
        {
            get => _firePosition.position;
            set => _firePosition.position= value;

        }
        public Vector3 Forward
        {
            get => _tryTf.forward;
        }
        public Quaternion Rotation
        {
            get => _tryTf.localRotation;
            set => _tryTf.localRotation = value;
        }
        public ParticleSystem ParticleSystem
        {
            get => _particleSystem;
        }
        [SerializeField] private Transform _firePosition;
        private Transform _tryTf => _tf ??= GetComponent<Transform>();
        private Transform _tf;
        private ParticleSystem _particleSystem => particleSystem??=GetComponentInChildren<ParticleSystem>();
        private ParticleSystem particleSystem;

        private void OnDrawGizmos()
        {

            if (Physics.Raycast(_firePosition.position, transform.forward, out RaycastHit hit))
            {
                Gizmos.DrawLine(_firePosition.position, hit.point);
                Gizmos.DrawSphere(hit.point, 0.05f);
            }
        }
    }

}
