using UnityEngine;

namespace Runtime
{
    public class WallRunCommand
    {
        public WallRunCommand(
            bool isWallRunning,
            bool useGravity,
            Vector3 wallForward,
            float forwardForce,
            bool overrideVerticalVelocity,
            float verticalVelocity,
            bool applyStickForce,
            Vector3 wallNormal,
            float stickForce)
        {
            IsWallRunning = isWallRunning;
            UseGravity = useGravity;
            WallForward = wallForward;
            ForwardForce = forwardForce;
            OverrideVerticalVelocity = overrideVerticalVelocity;
            VerticalVelocity = verticalVelocity;
            ApplyStickForce = applyStickForce;
            WallNormal = wallNormal;
            StickForce = stickForce;
        }

        public bool IsWallRunning { get; private set; }
        public bool UseGravity { get; private set; }
        public Vector3 WallForward { get; private set; }
        public float ForwardForce { get; private set; }
        public bool OverrideVerticalVelocity { get; private set; }
        public float VerticalVelocity { get; private set; }
        public bool ApplyStickForce { get; private set; }
        public Vector3 WallNormal { get; private set; }
        public float StickForce { get; private set; }
    }
}
