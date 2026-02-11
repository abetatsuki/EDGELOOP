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
            float wallRunSpeed,
            float wallRunDuration)
        {
            JumpPower = jumpPower;
            MoveSpeed = moveSpeed;
            DashPower = dashPower;
            RunMinRatio = runMinRatio;
            RunScrollStep = runScrollStep;
            WallRunSpeed = wallRunSpeed;
            WallRunDuration = wallRunDuration;
        }

        public float JumpPower { get; private set; }
        public float MoveSpeed { get; private set; }
        public float DashPower { get; private set; }
        public float RunMinRatio { get; private set; }
        public float RunScrollStep { get; private set; }
        public float WallRunSpeed { get; private set; }
        public float WallRunDuration { get; private set; }
    }
}
