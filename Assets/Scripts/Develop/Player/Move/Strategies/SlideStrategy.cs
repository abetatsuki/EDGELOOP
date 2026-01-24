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

        }


        private readonly float _slideDamping;
        private readonly float _endSpeed;
    }

}
