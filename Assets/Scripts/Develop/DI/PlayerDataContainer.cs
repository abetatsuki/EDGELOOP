using Develop.Interface;
using Develop.Player;
using Develop.Player.Camera;
using Develop.Player.Entity;
using Develop.Player.Move.Strategies;
using Develop.Player.Usecase;

namespace Develop.DI
{
    public class PlayerDataContainer
    {
        public PlayerPresenter PlayerPresenter { get; private set; }
        public HealthEntity HealthEntity { get; private set; }

        public void Init(IMovableBody body, PlayerConfig config)
        {
            HealthEntity = new HealthEntity(100);
            var playerEntity = new PlayerEntity();
            var cameralook = new CameraLook(body, config.LookSpeed, config.MaxAngle);
            var walkStrategy = new WalkStrategy(config.WalkSpeed);
            var runStrategy = new RunStrategy(config.RunSpeed);
            var slideStrategy = new SlideStrategy(config.DecelerationRate, config.EndSpeed, config.GroundLayer, config.GroundCheckDistance);
            var movePlayer = new MovePlayerUseCase(playerEntity, body, walkStrategy, runStrategy, slideStrategy, cameralook);
            PlayerPresenter = new PlayerPresenter(movePlayer, HealthEntity);
        }
    }
}
