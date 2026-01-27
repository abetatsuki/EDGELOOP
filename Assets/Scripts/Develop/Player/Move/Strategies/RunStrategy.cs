using Develop.Interface;
using UnityEngine;

namespace Develop.Player.Move.Strategies
{
    public class RunStrategy : IMovementStrategy
    {


        public RunStrategy(float speed)
        {
            _speed = speed;
        }

        public void Move(IMovableBody body, Vector2 input, float deltaTime)
        {
            body.LinearDamping = 0f;
            Vector3 inputDirection = new Vector3(input.x, 0, input.y).normalized;
            Vector3 worldDirection = body.PlayerQuaternion * inputDirection;
            Vector3 currentVelocity = body.Velocity;
            body.Velocity = new Vector3(worldDirection.x * _speed, currentVelocity.y, worldDirection.z * _speed);
        }
        private readonly float _speed;
    }
}
