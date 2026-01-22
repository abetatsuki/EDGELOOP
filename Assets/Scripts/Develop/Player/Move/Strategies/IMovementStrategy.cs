using Develop.Interface;
using UnityEngine;

namespace Develop.Player.Move.Strategies
{
    public interface IMovementStrategy
    {
        void Move(IMovableBody body, Vector2 input, float deltaTime);
    }
}
