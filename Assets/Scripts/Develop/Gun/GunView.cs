using Develop.Gun.Interface;
using UnityEngine;
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
            get => _firePosition.localPosition;
            set => _firePosition.localPosition = value;

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
        [SerializeField] private Transform _firePosition;
        private Transform _tryTf => _tf ??= GetComponent<Transform>();
        private Transform _tf;
    }

}
