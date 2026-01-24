using Develop.Interface;
using Develop.Player.Entity;
using Develop.Player.Move.Strategies;
using Develop.Player.Usecase;
using UnityEngine;

namespace Develop.Player
{
    public class PlayerPresenter : IPlayerInputPort,IPlayerUpdatable
    {

        public PlayerPresenter(MovePlayerUseCase move)
        {
            _movePlayerUseCase = move;
        }
           
        public void Update() { }
        public void OnMoveInput(Vector2 input, float deltaTime)
        {
            _movePlayerUseCase.Move(input,deltaTime);
        }

        public void OnRunInput(bool isRunning)
        {
           _movePlayerUseCase.SetRunning(isRunning);
        }

        private readonly MovePlayerUseCase _movePlayerUseCase;
    }
}