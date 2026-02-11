namespace Runtime
{
    public struct CharacterConfigData
    {
        public CharacterConfigData(float jumpPower, float moveSpeed, float dashPower)
        {
            JumpPower = jumpPower;
            MoveSpeed = moveSpeed;
            DashPower = dashPower;
        }

        public float JumpPower { get; private set; }
        public float MoveSpeed { get; private set; }
        public float DashPower { get; private set; }
    }
}
