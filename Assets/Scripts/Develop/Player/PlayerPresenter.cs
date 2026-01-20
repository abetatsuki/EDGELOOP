using Develop.Interface;
using UnityEngine;

public class PlayerPresenter  : IPlayerInputPort
{
   
    public PlayerPresenter(IMover mover)
    {
        _mover = mover;
    }

    public void OnMoveInput(Vector2 input)
    {
        Vector3 worldMoveInput = new Vector3(input.x, 0f, input.y);
        _mover?.OnMove(worldMoveInput);
    }
    private IMover _mover;
}
