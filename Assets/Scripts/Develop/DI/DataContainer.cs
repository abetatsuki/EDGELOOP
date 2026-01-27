using Develop.Interface;
using Develop.Player;
using Develop.Player.Camera;
using Develop.Player.Entity;
using Develop.Player.Move.Strategies;
using Develop.Player.Usecase;
using Develop.UI;

namespace Develop.DI
{
    public class DataContainer
    {
        public PlayerPresenter PlayerPresenter { get; private set; }
        public InputBuffer InputBuffer { get; private set; }

        public void Init(IMovableBody body, PlayerConfig config,IPlayer player)
        {
            var playerEntity = new PlayerEntity();
            var cameralook = new CameraLook(body,config.LookSpeed,config.MaxAngel);
            var walkStrategy = new WalkStrategy(config.WalkSpeed);
            var runStrategy = new RunStrategy(config.RunSpeed);
            var slideStrategy = new SlideStrategy(config.DecelerationRate,config.EndSpeed,config.GroundLayer,config.GroundCheckDistance);
            var movePlayer = new MovePlayerUseCase(playerEntity,body,walkStrategy,runStrategy,slideStrategy,cameralook);
            PlayerPresenter = new PlayerPresenter(movePlayer);
            InputBuffer = new InputBuffer(player,PlayerPresenter);
        }
    }
}