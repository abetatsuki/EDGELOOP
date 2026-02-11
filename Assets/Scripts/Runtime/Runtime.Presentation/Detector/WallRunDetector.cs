using UnityEngine;
using VContainer;

namespace Runtime
{
    [RequireComponent(typeof(Collider))]
    public class WallRunDetector : MonoBehaviour
    {
        [Inject] private IWallRunInputPort _wallRunInput;

        private void OnCollisionEnter(Collision collision)
        {
            if (!collision.collider.CompareTag("Wall"))
            {
                return;
            }
            HandleContact(collision);
        }

        private void OnCollisionStay(Collision collision)
        {
            if (!collision.collider.CompareTag("Wall"))
            {
                return;
            }
            HandleContact(collision);
        }

        private void OnCollisionExit(Collision collision)
        {
            if (!collision.collider.CompareTag("Wall"))
            {
                return;
            }
            _wallRunInput.EndWallRun();
        }

        private void HandleContact(Collision collision)
        {
            ContactPoint contact = collision.GetContact(0);
            Vector3 normal = contact.normal;
            // Determine if wall is on left or right relative to forward
            float side = Vector3.Dot(normal, transform.right);
            bool isLeftSide = side > 0f; // normal pointing to right means wall on left
            _wallRunInput.BeginWallRun(new WallRunContactData(isLeftSide));
        }
    }
}
