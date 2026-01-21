using Develop.Interface;
using Develop.Player;

namespace Develop.Data
{
    public class DataContainer 
    {
        public DataContainer(IMover mover)
        {
            Mover = mover;
        }
        
        public IMover Mover { get; private set; }

    }
}

