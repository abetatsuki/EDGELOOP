using Develop.Data;
using Develop.UI;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Awake()
    {
        _dataContainer = new DataContainer();
        _dataContainer.Init();
        _inputBuffer.Init(_dataContainer);
    }

    [SerializeField] private InputBuffer _inputBuffer;
    private DataContainer _dataContainer;
}
