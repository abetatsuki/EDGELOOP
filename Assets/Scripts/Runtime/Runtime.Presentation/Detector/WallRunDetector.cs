using UnityEngine;
using VContainer;

namespace Runtime
{
    [RequireComponent(typeof(Collider))]
    public class WallRunDetector : MonoBehaviour
    {
        [Inject] private IWallRunInputPort _wallRunInput;

private Vector2 direction;
private float maxDistance;
private LayerMask WallLayer;
private bool isWallRunning;
        public void GroundCheck()
        {
            if (Physics.Raycast(transform.position, direction, maxDistance, WallLayer))
            {
                isWallRunning = true;
            }
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
