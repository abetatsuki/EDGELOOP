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
            IMovementStrategy slide,
            ILook look)
        {
            _playerEntity = player;
            _body = body;
            _walk = walk;
            _run = run;
            _slide = slide;
            _look = look;
            _current = _walk;
        }

        public void Move(Vector2 input, float deltaTime)
        {
            if (input == Vector2.zero)
            {
                //ここにIdle処理を描く。
            }
            if (_playerEntity.IsSliding)
            {
                _current.Move(_body, Vector2.zero, deltaTime);
            }
            else if (_playerEntity.CanMove())
            {
                _current.Move(_body, input, deltaTime);
            }
        }

        public void Look(Vector2 input)
        {
            _look.Look(input);
        }
        public void Slide(bool isSliding)
        {
            if (isSliding)
            {
                _playerEntity.StartSliding();
                _current = _slide;
            }
            else if (!isSliding)
            {
                _playerEntity.StopSliding();
                _current = _walk;
            }
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
        private ILook _look;
    }
}

