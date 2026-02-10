using Runtime;
using UnityEngine;
using VContainer;

public class EnviromentAdaptor : MonoBehaviour
{
    
    [Inject] private IApplyEnvironmentState _applyEnvironmentState;


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
