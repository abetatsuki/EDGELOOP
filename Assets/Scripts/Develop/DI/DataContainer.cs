using Develop.Interface;
using Develop.Player;
using Develop.Player.Entity;
using Develop.Player.Move.Strategies;
using Develop.Player.View;

namespace Develop.DI
{
    public class DataContainer 
    {
        public PlayerPresenter PlayerPresenter { get; private set; }

        public void Init(IMovableBody body, PlayerConfig config)
        {
            var walkStrategy = new WalkStrategy(config.WalkSpeed);
            var runStrategy = new RunStrategy(config.RunSpeed);

            PlayerPresenter = new PlayerPresenter(body, walkStrategy, runStrategy);
        }
    }
}