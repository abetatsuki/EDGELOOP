using Runtime;
using UnityEngine;

public class EnviromentAdaptor : MonoBehaviour
{
    private IEnvironmentInputPort _applyEnvironmentState;

    public void SetEnvironmentInputPort(IEnvironmentInputPort applyEnvironmentState)
    {
        _applyEnvironmentState = applyEnvironmentState;
    }

    private void OnCollisionEnter(Collision collision)
    {
        _applyEnvironmentState?.ApplyEnvironment(
            new EnvironmentInputData(true)
        );
    }

    private void OnCollisionExit(Collision collision)
    {
        _applyEnvironmentState?.ApplyEnvironment(
            new EnvironmentInputData(false)
        );
    }
}
