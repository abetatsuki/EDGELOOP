using System.Collections.Generic;

namespace Runtime
{
    public static class RuntimeServiceRegistry
    {
        public static IPlayerRuntimeFactory PlayerRuntimeFactory { get; set; }
        public static IReadOnlyList<IMoveSpeedUiInput> MoveSpeedUiInputs { get; set; }
    }
}
