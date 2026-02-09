using UnityEngine;
namespace Runtime
{
    public interface IApplyEnvironmentState
    {
       public void ApplyEnvironment(Environment environment);
    }
    public struct Environment
    {
        public Environment(bool isGround)
        {
            IsGround = isGround;
        }
        public bool IsGround { get ; private set; }
    }
}