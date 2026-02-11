namespace Runtime
{
    public class WallRun : IWallRunInputPort
    {
        private readonly CharacterConfigData _config;
        private readonly IWallRunPhysicsOutput _physicsOutput;

        public WallRun(CharacterConfigData config, IWallRunPhysicsOutput physicsOutput)
        {
            _config = config;
            _physicsOutput = physicsOutput;
        }

        public void BeginWallRun(WallRunContactData data)
        {
            WallSide side = data.IsLeftSide ? WallSide.Left : WallSide.Right;
            float roll = side == WallSide.Left ? 20f : -20f;
            _physicsOutput.BeginWallRun(new WallRunCommand(side, _config.WallRunSpeed, _config.WallRunDuration, roll));
        }

        public void EndWallRun()
        {
            _physicsOutput.EndWallRun();
        }
    }
}
