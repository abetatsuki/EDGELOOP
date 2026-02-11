namespace Runtime
{
    public struct CharacterConfigData
    {
        public CharacterConfigData(
            float jumpPower,
            float moveSpeed,
            float dashPower,
            float runMinRatio,
            float runScrollStep)
        {
            JumpPower = jumpPower;
            MoveSpeed = moveSpeed;
            DashPower = dashPower;
            RunMinRatio = runMinRatio;
            RunScrollStep = runScrollStep;
        }

        public float JumpPower { get; private set; }
        public float MoveSpeed { get; private set; }
        public float DashPower { get; private set; }
        public float RunMinRatio { get; private set; }
        public float RunScrollStep { get; private set; }
    }
}
