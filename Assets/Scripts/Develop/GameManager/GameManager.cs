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
        [SerializeField] private InputBuffer _inputBuffer;
        [SerializeField] private PlayerView _playerView;
        [SerializeField] private PlayerConfig _playerConfig;

        private DataContainer _dataContainer;
        private void Awake()
        {
            _dataContainer = new DataContainer();
            // PlayerView implements IMovableBody
            _dataContainer?.Init(_playerView, _playerConfig);
            _inputBuffer?.Init(_dataContainer.PlayerPresenter);
        }
        private void Update()
        {
            _dataContainer?.PlayerPresenter.Update();
        }
    }
}
