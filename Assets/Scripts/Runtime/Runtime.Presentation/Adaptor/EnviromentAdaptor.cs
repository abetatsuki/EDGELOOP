using Runtime;
using UnityEngine;
using VContainer;

public class EnviromentAdaptor : MonoBehaviour
{
    
    [Inject] private IApplyEnvironmentState _applyEnvironmentState;


    void OnCollisionEnter(Collision collision)
    {
          _applyEnvironmentState?.ApplyEnvironment(
            new Environment(true)
        );
    }
    void OnCollisionExit(Collision collision)
    {
          _applyEnvironmentState?.ApplyEnvironment(
            new Environment(false)
        );
    }
}
