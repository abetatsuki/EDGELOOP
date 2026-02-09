using UnityEngine;

namespace Runtime
{
    public interface IJumpPhysicsOutput
    {
        void ApplyJump(JumpCommand command);
    }
}
