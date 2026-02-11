namespace Runtime
{
    public interface IWallRunInputPort
    {
        void BeginWallRun(WallRunContactData data);
        void EndWallRun();
    }

    public struct WallRunContactData
    {
        public WallRunContactData(bool isLeftSide)
        {
            IsLeftSide = isLeftSide;
        }

        public bool IsLeftSide { get; private set; }
    }
}
