using UnityEngine;

namespace Runtime
{
    public interface IWallSenseInputPort
    {
        void Publish(WallSenseData data);
    }

    public struct WallSenseData
    {
        public WallSenseData(
            bool hasLeftWall,
            bool hasRightWall,
            Vector3 leftWallNormal,
            Vector3 rightWallNormal,
            bool isAboveGround)
        {
            HasLeftWall = hasLeftWall;
            HasRightWall = hasRightWall;
            LeftWallNormal = leftWallNormal;
            RightWallNormal = rightWallNormal;
            IsAboveGround = isAboveGround;
        }

        public bool HasLeftWall { get; private set; }
        public bool HasRightWall { get; private set; }
        public Vector3 LeftWallNormal { get; private set; }
        public Vector3 RightWallNormal { get; private set; }
        public bool IsAboveGround { get; private set; }
    }
}
