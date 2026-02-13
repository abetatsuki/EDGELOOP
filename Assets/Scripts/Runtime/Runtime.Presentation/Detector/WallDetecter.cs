using UnityEngine;
using VContainer;

namespace Runtime
{
    public class WallDetecter : MonoBehaviour
    {
        [SerializeField] private Transform _orientation;
        [SerializeField] private LayerMask _wall;
        [SerializeField] private LayerMask _ground;
        [SerializeField] private float _wallCheckDistance = 1f;
        [SerializeField] private float _minJumpHeight = 1f;

        [Inject] private IWallSenseInputPort _wallSenseInputPort;

        private bool _wallRight;
        private bool _wallLeft;
        private RaycastHit _rightWallHit;
        private RaycastHit _leftWallHit;

        private void Update()
        {
            CheckForWall();
            bool isAboveGround = AboveGround();

            _wallSenseInputPort?.Publish(new WallSenseData(
                _wallLeft,
                _wallRight,
                _wallLeft ? _leftWallHit.normal : Vector3.zero,
                _wallRight ? _rightWallHit.normal : Vector3.zero,
                isAboveGround));
        }

        private void CheckForWall()
        {
            if (_orientation == null)
            {
                _wallRight = false;
                _wallLeft = false;
                return;
            }

            _wallRight = Physics.Raycast(transform.position, _orientation.right, out _rightWallHit, _wallCheckDistance, _wall);
            _wallLeft = Physics.Raycast(transform.position, -_orientation.right, out _leftWallHit, _wallCheckDistance, _wall);
        }

        private bool AboveGround()
        {
            return !Physics.Raycast(transform.position, Vector3.down, _minJumpHeight, _ground);
        }
    }
}
