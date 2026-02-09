namespace Runtime
{
    public struct CharacterConfigData
    {
        public CharacterConfigData(float jumpPower)
        {
            JumpPower = jumpPower;
        }

        public float JumpPower { get; private set; }
    }
}
