using UnityEngine;
using VContainer;

namespace Runtime
{
    public class WallRunDriver : MonoBehaviour
    {
        [Inject] private IWallRunTickInputPort _wallRunTickInputPort;

        private void FixedUpdate()
        {
            _wallRunTickInputPort.Tick(Time.fixedDeltaTime);
        }
    }
}
