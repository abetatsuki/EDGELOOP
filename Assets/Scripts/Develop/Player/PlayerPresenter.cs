using System;
using Develop.Interface;
using Develop.Player.Entity;
using Develop.Player.Usecase;
using UnityEngine;

namespace Develop.Player
{
    public class PlayerPresenter : IPlayerInputPort, IPlayerUpdatable
    {
        public PlayerPresenter(MovePlayerUseCase move, HealthEntity healthEntity)
        {
            _movePlayerUseCase = move;
            _healthEntity = healthEntity;

            _healthEntity.OnHealthChanged += health => OnHealthChanged?.Invoke(health);
        }

        public event Action<int> OnHealthChanged;
        
        public int CurrentHealth => _healthEntity.CurrentHealth;
        public int MaxHealth => _healthEntity.MaxHealth;
        
        public void Update() { }

        public void TakeDamage(int damage)
        {
            _healthEntity.TakeDamage(damage);
        }
        
        public void OnMoveInput(Vector2 input, float deltaTime)
        {
            _movePlayerUseCase.Move(input, deltaTime);
        }

        public void OnRunInput(bool isRunning)
        {
            _movePlayerUseCase.SetRunning(isRunning);
        }

        public void OnSlideInput(bool isSliding)
        {
            _movePlayerUseCase.Slide(isSliding);
        }
        public void OnLookInput(Vector2 input)
        {
            _movePlayerUseCase.Look(input);
        }

        private readonly MovePlayerUseCase _movePlayerUseCase;
        private readonly HealthEntity _healthEntity;
    }
}