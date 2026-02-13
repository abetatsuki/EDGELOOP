using UnityEngine;

namespace Runtime
{
    public interface IWallRunInputPort
    {
        void Handle(WallRunInputData data);
    }

    public struct WallRunInputData
    {
        public float MoveX;
        public float MoveY;
        public bool IsClimbPressed;
        public bool IsDescendPressed;
    }
}
