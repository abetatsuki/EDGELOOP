using Develop.Data;
using Develop.Interface;
using Develop.Player;
using Develop.UI;
using UnityEngine;
[DefaultExecutionOrder(-100)]
public class GameManager : MonoBehaviour
{
    [SerializeField] private InputBuffer _inputBuffer;
    private IMover _mover;
    private IPlayerInputReceiver _receiver;
    private IPlayerUpdatable _updatable;
    private void Awake()
    {
        _mover = new Move();
        var presenter = new PlayerPresenter(_mover);
        _receiver = presenter;
        _updatable = presenter;

        _inputBuffer?.Init(_receiver);

    }
    private void Update()
    {
        _updatable.Update();
    }

}
