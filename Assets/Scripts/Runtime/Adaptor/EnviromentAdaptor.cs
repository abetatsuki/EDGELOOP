using Runtime;
using UnityEngine;

public class EnviromentAdaptor : MonoBehaviour
{
    public void SetApplyEnvironmentState(IApplyEnvironmentState applyEnvironmentState)
    {
        _applyEnvironmentState = applyEnvironmentState;
    }
    [SerializeField] GroundDetector _groundDetector;
    private IApplyEnvironmentState _applyEnvironmentState;

    private void Update()
    {
        _applyEnvironmentState?.ApplyEnvironment(
            new Environment(_groundDetector.IsGround)
        );
    }
}
