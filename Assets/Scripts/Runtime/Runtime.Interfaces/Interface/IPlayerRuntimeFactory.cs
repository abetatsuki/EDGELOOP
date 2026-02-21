using UnityEngine;

namespace Runtime
{
    public interface IPlayerRuntimeFactory
    {
        PlayerRuntimeBindings Create(GameObject playerRoot);
    }

    public class PlayerRuntimeBindings
    {
        public PlayerRuntimeBindings(
            IJumpInputPort jumpPort,
            IMoveInputPort movePort,
            IDashInputPort dashPort,
            ISlideInputPort slidePort,
            IRunSpeedInputPort runSpeedPort,
            ILookInputPort lookPort,
            IWallRunInputPort wallRunInputPort,
            IWallRunTickInputPort wallRunTickInputPort,
            IWallSenseInputPort wallSenseInputPort,
            IWallSenseInputPort additionalWallSenseInputPort,
            IEnvironmentInputPort environmentInputPort)
        {
            JumpPort = jumpPort;
            MovePort = movePort;
            DashPort = dashPort;
            SlidePort = slidePort;
            RunSpeedPort = runSpeedPort;
            LookPort = lookPort;
            WallRunInputPort = wallRunInputPort;
            WallRunTickInputPort = wallRunTickInputPort;
            WallSenseInputPort = wallSenseInputPort;
            AdditionalWallSenseInputPort = additionalWallSenseInputPort;
            EnvironmentInputPort = environmentInputPort;
        }

        public IJumpInputPort JumpPort { get; private set; }
        public IMoveInputPort MovePort { get; private set; }
        public IDashInputPort DashPort { get; private set; }
        public ISlideInputPort SlidePort { get; private set; }
        public IRunSpeedInputPort RunSpeedPort { get; private set; }
        public ILookInputPort LookPort { get; private set; }
        public IWallRunInputPort WallRunInputPort { get; private set; }
        public IWallRunTickInputPort WallRunTickInputPort { get; private set; }
        public IWallSenseInputPort WallSenseInputPort { get; private set; }
        public IWallSenseInputPort AdditionalWallSenseInputPort { get; private set; }
        public IEnvironmentInputPort EnvironmentInputPort { get; private set; }
    }
}
