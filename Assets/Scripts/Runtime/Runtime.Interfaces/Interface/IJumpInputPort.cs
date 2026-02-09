using UnityEngine;

namespace Runtime
{
    public interface IJumpInputPort
    {
        void Handle(JumpInputData data);
    }
    public struct JumpInputData
    {
        public bool IsPressed;
    }
}
