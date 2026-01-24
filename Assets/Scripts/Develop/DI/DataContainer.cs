using Develop.Interface;
using Develop.Player;
using Develop.Player.Entity;
using Develop.Player.Move.Strategies;
using Develop.Player.Usecase;

namespace Develop.DI
{
    public class DataContainer
    {
        public PlayerPresenter PlayerPresenter { get; private set; }

        public void Init(IMovableBody body, PlayerConfig config)
        {
            var playerEntity = new PlayerEntity();
            var walkStrategy = new WalkStrategy(config.WalkSpeed);
            var runStrategy = new RunStrategy(config.RunSpeed);
            var slideStrategy = new SlideStrategy(config.Damping,config.SlidingEnd);
            var movePlayer = new MovePlayerUseCase(playerEntity,body,walkStrategy,runStrategy,slideStrategy);
            PlayerPresenter = new PlayerPresenter(movePlayer);
        }
    }
}