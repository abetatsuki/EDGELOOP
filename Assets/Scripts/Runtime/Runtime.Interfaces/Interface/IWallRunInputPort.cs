namespace Runtime
{
    public interface IWallRunInputPort
    {
        void BeginWallRun(WallRunContactData data);
        void EndWallRun();
    }

    public struct WallRunContactData
    {
        public WallRunContactData(bool isLeftSide, bool isWallRun)
        {
            IsLeftSide = isLeftSide;
            IsWallRun = isWallRun;
        }

　　　　public bool IsWallRun { get; private set; }
        public bool IsLeftSide { get; private set; }
    }
}
