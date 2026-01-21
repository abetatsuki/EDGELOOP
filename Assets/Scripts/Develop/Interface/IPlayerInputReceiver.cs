using UnityEngine;
namespace Develop.Interface
{
    public interface IPlayerInputReceiver
    {
        void OnMoveInput(Vector2 input);
    }
}
