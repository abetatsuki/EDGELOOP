using Develop.DI;
using Develop.Gun;
using Develop.Player.Entity;
using Develop.Player.View;
using Develop.UI;
using UnityEngine;

namespace Develop.GameManager
{
    [DefaultExecutionOrder(-100)]
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private PlayerView _playerView;
        [SerializeField] private GunView _gunView;
        [SerializeField] private GunConfig _gunConfig;
        [SerializeField] private PlayerConfig _playerConfig;
        
        private PlayerDataContainer _playerDataContainer;
        private GunDataContainer _gunDataContainer;
        private DataContainer _dataContainer;

        private void Awake()
        {
            _playerDataContainer = new PlayerDataContainer();
            _playerDataContainer.Init(_playerView, _playerConfig);

            _gunDataContainer = new GunDataContainer();
            _gunDataContainer.Init(_gunConfig, _gunView);

            _dataContainer = new DataContainer(_playerDataContainer, _gunDataContainer);
            _dataContainer.Init(_playerView);

            _playerView.Init(_playerDataContainer.PlayerPresenter, _dataContainer.InputBuffer);
            _gunView.Init(_gunDataContainer.GunPresenter);
        }
    }
}
