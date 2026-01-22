using Develop.Interface;
using Develop.Player;
using Develop.Player.Move;

namespace Develop.DI
{
    public class DataContainer 
    {
        public DataContainer() {}
        
        public IMover Mover { get; private set; }
        public PlayerPresenter PlayerPresenter { get; private set; }


        public void Init()
        {
            Mover = new MovementService();
            PlayerPresenter = new PlayerPresenter(Mover);
        }
    }
}

