using UnityEngine;

namespace Develop.Player.State
{
    public class PlayerStatus : IPlayerState
    {
        public PlayerStatus()
        {
            PlayerState = new PlayerState();
           // PlayerState = PlayerState.Idle;
        }
        public PlayerState PlayerState { get; private set; }
        public void ChangeState(PlayerState state)
        {
            PlayerState = state;
        }

    }
}
