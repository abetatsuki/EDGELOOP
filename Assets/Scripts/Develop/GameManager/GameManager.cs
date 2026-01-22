using Develop.DI;
using Develop.UI;
using UnityEngine;

namespace Develop.GameManager
{
    [DefaultExecutionOrder(-100)]
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private InputBuffer _inputBuffer;
        private DataContainer _dataContainer;
        private void Awake()
        {
            _dataContainer = new DataContainer();
            _dataContainer?.Init();
            _inputBuffer?.Init(_dataContainer.PlayerPresenter);
        }
        private void Update()
        {
            _dataContainer?.PlayerPresenter.Update();
        }
    }
}
