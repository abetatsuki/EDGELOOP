using Develop.Interface;
using UnityEngine;
namespace Develop.Player.Move.Strategies
{
    public class SlideStrategy : IMovementStrategy
    {
        public SlideStrategy(float slideDamping, float endSpeed)
        {
            _slideDamping = slideDamping;
            _endSpeed = endSpeed;
        }
        public void Move(IMovableBody body, Vector2 input, float deltaTime)
        {
            body.LinearDamping = _slideDamping;
        }
        public bool IsFinished(IMovableBody body)
        {
            Vector3 horizontalVelocity = new Vector3(
                body.Velocity.x,
                0.0f,
                body.Velocity.z
            );

            return horizontalVelocity.magnitude < _endSpeed;
        }

        private readonly float _slideDamping;
        private readonly float _endSpeed;
    }

}
