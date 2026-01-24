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

        // 走るかどうかを切り替える
        public void SetRunning(bool isRunning)
        {
            if (_current == _slide) return;
            _current = isRunning ? _run : _walk;
        }

        // 移動を実行する
        public void Move(Vector2 input, float deltaTime)
        {
            _current.Move(_body, input, deltaTime);
        }

        private readonly IMovableBody _body;
        private readonly IMovementStrategy _walk;
        private readonly IMovementStrategy _run;
        private readonly IMovementStrategy _slide;
        private readonly PlayerEntity _playerEntity;
        private IMovementStrategy _current;
    }
}

