using UnityEngine;
using VContainer;

namespace Runtime
{
    [RequireComponent(typeof(Collider))]
    public class WallRunAdapter : MonoBehaviour
    {
        [Inject] private IWallRunInputPort _wallRunInput;

        private Vector2 direction;
        private float maxDistance;
        private LayerMask WallLayer;
        private LayerMask GroundLayer;

        private RaycastHit leftHit;
        private RaycastHit rightHit;
        private bool leftwall;
        private bool rightwall;
        
        private bool isWallRunning;
        public void CheckEnvironment()
        {
            Vector3 origin = transform.position + Vector3.up * 1.0f;

            leftwall = Physics.Raycast(origin, -transform.right, out leftHit, maxDistance, WallLayer);
            rightwall = Physics.Raycast(origin, transform.right, out rightHit, maxDistance, WallLayer);

            
            
        }

private bool IsGroundNear()
    {
        // 停止判定用（地面が近いなら止める）
        return Physics.Raycast(transform.position, Vector3.down, maxDistance, GroundLayer);
    }
        private void HandleContact(Collision collision)
        {
            ContactPoint contact = collision.GetContact(0);
            Vector3 normal = contact.normal;
            // Determine if wall is on left or right relative to forward
            float side = Vector3.Dot(normal, transform.right);
            bool isLeftSide = side > 0f; // normal pointing to right means wall on left
            _wallRunInput.BeginWallRun(new WallRunContactData(isLeftSide,isWallRunning));
        }
    }
}
