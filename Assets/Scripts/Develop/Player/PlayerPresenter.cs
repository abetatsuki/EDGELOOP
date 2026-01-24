using Develop.Interface;
using Develop.Player.Entity;
using Develop.Player.Move.Strategies;
using UnityEngine;

namespace Develop.Player
{
    public class PlayerPresenter : IPlayerInputPort,IPlayerUpdatable
    {

        public PlayerPresenter(
            IMovableBody body,
            IMovementStrategy walkStrategy,
            IMovementStrategy runStrategy)
        {
            _body = body;
            _walkStrategy = walkStrategy;
            _runStrategy = runStrategy;
            _currentStrategy = _walkStrategy;
        }
        public void Update() { }
        public void OnMoveInput(Vector2 input, float deltaTime)
        {
            _currentStrategy.Move(_body, input, deltaTime);
        }

        public void OnRunInput(bool isRunning)
        {
            _currentStrategy = isRunning ? _runStrategy : _walkStrategy;
        }

        private readonly IMovableBody _body;

        private IMovementStrategy _currentStrategy;
        private readonly IMovementStrategy _walkStrategy;
        private readonly IMovementStrategy _runStrategy;
    }
}