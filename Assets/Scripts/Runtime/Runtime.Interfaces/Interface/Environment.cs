using UnityEngine;
namespace Runtime
{
    public interface IEnvironmentInputPort
    {
       public void ApplyEnvironment(EnvironmentInputData environment);
    }
    public struct EnvironmentInputData
    {
        public EnvironmentInputData(bool isGround)
        {
            IsGround = isGround;
        }
        public bool IsGround { get ; private set; }
    }
}
