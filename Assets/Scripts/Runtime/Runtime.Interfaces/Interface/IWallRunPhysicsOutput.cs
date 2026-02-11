namespace Runtime
{
    public interface IWallRunPhysicsOutput
    {
        void BeginWallRun(WallRunCommand command);
        void EndWallRun();
    }
}
