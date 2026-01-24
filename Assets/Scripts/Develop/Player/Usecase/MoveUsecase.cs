using Develop.Interface;
using Develop.Player.Entity;
using Develop.Player.Move.Strategies;
using UnityEngine;

namespace Develop.Player.Usecase
{
    public class MovePlayerUseCase
    {

        public MovePlayerUseCase(
            PlayerEntity player,
            IMovableBody body,
            IMovementStrategy walk,
            IMovementStrategy run,
            IMovementStrategy slide)
        {
            _playerEntity = player;
            _body = body;
            _walk = walk;
            _run = run;
            _slide = slide;
            _current = _walk;
        }

        public void Move(Vector2 input, float deltaTime)
        {
            _current.Move(_body, input, deltaTime);
        }
        public void Slide()
        {
            if (_playerEntity.IsJumping) return;

            Vector3 velocity = new Vector3(_body.Velocity.x, 0, _body.Velocity.z);

            if (velocity.magnitude < 1) return;

            _current = _slide; 
        }
        public void SetRunning(bool isRunning)
        {
            if (_current == _slide) return;
            _current = isRunning ? _run : _walk;
        }


        private readonly IMovableBody _body;
        private readonly IMovementStrategy _walk;
        private readonly IMovementStrategy _run;
        private readonly IMovementStrategy _slide;
        private readonly PlayerEntity _playerEntity;
        private IMovementStrategy _current;
    }
}

