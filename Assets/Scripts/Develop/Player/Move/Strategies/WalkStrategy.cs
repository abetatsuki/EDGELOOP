using Develop.Interface;
using UnityEngine;

namespace Develop.Player.Move.Strategies
{
    public class WalkStrategy : IMovementStrategy
    {
        private readonly float _speed;

        public WalkStrategy(float speed)
        {
            _speed = speed;
        }

        public void Move(IMovableBody body, Vector2 input, float deltaTime)
        {
            Vector3 direction = new Vector3(input.x, 0, input.y).normalized;
            // 単純な移動計算
            body.Position += direction * _speed * deltaTime;
        }
    }
}
