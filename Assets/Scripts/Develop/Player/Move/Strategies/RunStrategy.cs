using Develop.Interface;
using Develop.Player.Entity;
using UnityEngine;

namespace Develop.Player.Move.Strategies
{
    public class RunStrategy : IMovementStrategy
    {
       

        public RunStrategy( float speed)
        {
            _speed = speed;
        }

        public void Move(IMovableBody body, Vector2 input, float deltaTime)
        {
            Vector3 direction = new Vector3(input.x, 0, input.y);
            body.Position += direction * _speed * deltaTime;
        }
        private readonly float _speed;
    }
}
