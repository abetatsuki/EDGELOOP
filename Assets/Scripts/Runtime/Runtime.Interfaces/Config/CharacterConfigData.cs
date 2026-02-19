using UnityEngine;

namespace Runtime
{
    public struct CharacterConfigData
    {
        public CharacterConfigData(
            float jumpPower,
            float moveSpeed,
            float dashPower,
            float runMinRatio,
            float runScrollStep,
            float wallRunForce,
            float wallClimbSpeed,
            float maxWallRunTime,
            float wallStickForce)
        {
            JumpPower = jumpPower;
            MoveSpeed = moveSpeed;
            DashPower = dashPower;
            RunMinRatio = runMinRatio;
            RunScrollStep = runScrollStep;
            WallRunForce = wallRunForce;
            WallClimbSpeed = wallClimbSpeed;
            MaxWallRunTime = maxWallRunTime;
            WallStickForce = wallStickForce;
        }

        public float JumpPower { get; private set; }
        public float MoveSpeed { get; private set; }
        public float DashPower { get; private set; }
        public float RunMinRatio { get; private set; }
        public float RunScrollStep { get; private set; }
        public float WallRunForce { get; private set; }
        public float WallClimbSpeed { get; private set; }
        public float MaxWallRunTime { get; private set; }
        public float WallStickForce { get; private set; }
    }
}
