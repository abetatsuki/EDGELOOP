using Develop.Interface;
using Develop.Player;

namespace Develop.Data
{
    public class DataContainer 
    {
        public DataContainer() {}
        
        public IMover Mover { get; private set; }
        public IPlayerInputPort PlayerPresenter { get; private set; }


        public void Init()
        {
            Mover = new Move();
            PlayerPresenter = new PlayerPresenter(Mover);
        }
    }
}

