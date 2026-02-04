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


        private DataContainer _dataContainer;
        private void Awake()
        {
            _dataContainer = new DataContainer();
            _dataContainer?.Init(_playerView, _playerConfig,_playerView,_gunConfig,_gunView);
            _playerView.Init(_dataContainer.PlayerPresenter,_dataContainer.InputBuffer,_dataContainer.HealthEntity);
            _gunView.Init(_dataContainer.GunPresenter);
        }
    }
}
