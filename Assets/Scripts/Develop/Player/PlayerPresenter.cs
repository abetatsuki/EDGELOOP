using Develop.Interface;
using UnityEngine;

namespace Develop.Player
{
    public class PlayerPresenter : IPlayerInputReceiver,IPlayerUpdatable
    {

        public PlayerPresenter(IMover mover)
        {
            _mover = mover;
        }
        public void Update() { }
        public void OnMoveInput(Vector2 input)
        {
            Vector3 worldMoveInput = new Vector3(input.x, 0f, input.y);
            _mover?.OnMove(worldMoveInput);
        }
        private IMover _mover;
    }
}
