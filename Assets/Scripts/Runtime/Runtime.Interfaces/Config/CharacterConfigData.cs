namespace Runtime
{
    public struct CharacterConfigData
    {
        public CharacterConfigData(float jumpPower, float moveSpeed)
        {
            JumpPower = jumpPower;
            MoveSpeed = moveSpeed;
        }

        public float JumpPower { get; private set; }
        public float MoveSpeed { get; private set; }
    }
}
