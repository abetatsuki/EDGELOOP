using Develop.Interface;
using UnityEngine;

namespace Develop.Player.Move.Strategies
{
    public class RunStrategy : IMovementStrategy
    {
        private readonly float _speed;

        public RunStrategy(float speed)
        {
            _speed = speed;
        }

        public void Move(IMovableBody body, Vector2 input, float deltaTime)
        {
            Vector3 direction = new Vector3(input.x, 0, input.y);
            body.Position += direction * _speed * deltaTime;
        }
    }
}
