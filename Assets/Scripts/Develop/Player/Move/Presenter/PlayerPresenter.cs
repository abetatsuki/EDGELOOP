using Develop.Interface;
using UnityEngine;

public class PlayerPresenter 
{
   
    public PlayerPresenter(IMover mover)
    {
        _mover = mover;
    }

    public void OnMove(Vector3 input)
    {
        _mover.OnMove(input);
    }
    private IMover _mover;
}
