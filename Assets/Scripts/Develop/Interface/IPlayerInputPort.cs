using UnityEngine;
namespace Develop.Interface
{
    public interface IPlayerInputPort
    {
        void OnMoveInput(Vector2 input, float deltaTime);
        void OnRunInput(bool isRunning);
        
        void OnSlideInput(bool isSliding);
    }
}