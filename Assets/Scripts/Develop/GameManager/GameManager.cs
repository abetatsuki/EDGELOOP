using Develop.DI;
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
        [SerializeField] private PlayerConfig _playerConfig;

        private DataContainer _dataContainer;
        private void Awake()
        {
            _dataContainer = new DataContainer();
            _dataContainer?.Init(_playerView, _playerConfig,_playerView);
            _playerView.Init(_dataContainer.PlayerPresenter,_dataContainer.InputBuffer);
        }
    }
}
