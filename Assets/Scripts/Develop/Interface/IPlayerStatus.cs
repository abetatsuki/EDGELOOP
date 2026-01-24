using UnityEngine;

public enum PlayerState
{
   Idle,
   Walking,
   Running,
}
public interface IPlayerState
{
    PlayerState PlayerState { get; }

    void ChangeState(PlayerState state);
}
