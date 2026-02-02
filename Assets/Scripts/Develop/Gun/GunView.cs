using Develop.Gun.Interface;
using UnityEngine;
namespace Develop.Gun
{
    public class GunView : MonoBehaviour, IGunView
    {
        public void Init(GunPresenter presenter)
        {
            _presenter = presenter;
        }
        public Vector2 LookInput { get; set; }
        public Quaternion TargetSwayRotation { get; set; }
        public Vector3 Position
        {
            get => _tryTf.localPosition;
            set => _tryTf.localPosition = value;
        }
        public Vector3 FirePosition
        {
            get => _firePosition.position;
            set => _firePosition.position = value;

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
        public ParticleSystem MuzzleFlash
        {
            get => _muzuleFlash;
        }
        public GameObject BulletHolePrefab
        {
            get => _bulletHolePrefab;
        }
        public Vector3 AimPosition
        {
            get => _aimPosition.localPosition;
            set => _aimPosition.localPosition= value;
        }
        public Vector3 DefaultPosition
        {
            get => _defaultPosition.localPosition;
            set => _defaultPosition.localPosition = value;
        }
        public Animator GunAnimator
        {
            get => _anim;
        }
        [SerializeField] private Transform _aimPosition;
        [SerializeField] private Transform _defaultPosition;
        [SerializeField] private Transform _firePosition;

        private Animator _animator;
        private Animator _anim => _animator ??= GetComponent<Animator>();
        private GunPresenter _presenter;
        private Transform _tryTf => _tf ??= GetComponent<Transform>();
        private Transform _tf;
        [SerializeField]
        private ParticleSystem _muzuleFlash;
        [SerializeField]
        private GameObject _bulletHolePrefab;

        private void Awake()
        {
            TargetSwayRotation = transform.localRotation;
        }

        private void OnDrawGizmos()
        {

            if (Physics.Raycast(FirePosition, Forward, out RaycastHit hit))
            {
                Gizmos.DrawLine(FirePosition, hit.point);
                Gizmos.DrawSphere(hit.point, 0.05f);
            }
        }
        private void Update()
        {
            _presenter.Update();
        }

        private void LateUpdate()
        {
            Rotation = TargetSwayRotation;
        }
    }

}
