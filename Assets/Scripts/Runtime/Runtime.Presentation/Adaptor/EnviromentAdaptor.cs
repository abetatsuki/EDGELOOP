using Runtime;
using UnityEngine;
using VContainer;

public class EnviromentAdaptor : MonoBehaviour
{
    [SerializeField] GroundDetector _groundDetector;
    [Inject] private IApplyEnvironmentState _applyEnvironmentState;

    private void Update()
    {
        _applyEnvironmentState?.ApplyEnvironment(
            new Environment(_groundDetector.IsGround)
        );
    }
}
