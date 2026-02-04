using Develop.Gun;
using Develop.Interface;
using Develop.Player;
using Develop.UI;

namespace Develop.DI
{
    public class DataContainer
    {
        public PlayerDataContainer PlayerDataContainer { get; }
        public GunDataContainer GunDataContainer { get; }
        public InputBuffer InputBuffer { get; private set; }

        public DataContainer(PlayerDataContainer playerDataContainer, GunDataContainer gunDataContainer)
        {
            PlayerDataContainer = playerDataContainer;
            GunDataContainer = gunDataContainer;
        }

        public void Init(IPlayer player)
        {
            InputBuffer = new InputBuffer(player, PlayerDataContainer.PlayerPresenter, GunDataContainer.GunPresenter);
        }
    }
}