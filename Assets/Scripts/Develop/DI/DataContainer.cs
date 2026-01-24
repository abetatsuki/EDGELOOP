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
            var PlayerEntity = new PlayerEntity();
            var walkStrategy = new WalkStrategy(config.WalkSpeed);
            var runStrategy = new RunStrategy(config.RunSpeed);
            var MovePlayer = new MovePlayerUseCase(body,walkStrategy,runStrategy);
            PlayerPresenter = new PlayerPresenter(MovePlayer);
        }
    }
}