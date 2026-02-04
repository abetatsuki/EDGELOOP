using System;
﻿using Develop.Interface;
﻿using Develop.Player.Entity;
﻿using Develop.Player.Usecase;
﻿using UnityEngine;
﻿
﻿namespace Develop.Player
﻿{
﻿    public class PlayerPresenter : IPlayerInputPort, IPlayerUpdatable
﻿    {
﻿        private readonly PlayerImpactUseCase _playerImpactUseCase; // Add PlayerImpactUseCase field
﻿
﻿        public PlayerPresenter(MovePlayerUseCase move, HealthEntity healthEntity, PlayerImpactUseCase playerImpactUseCase) // Add PlayerImpactUseCase to constructor
﻿        {
﻿            _movePlayerUseCase = move;
﻿            _healthEntity = healthEntity;
﻿            _playerImpactUseCase = playerImpactUseCase; // Initialize field
﻿
﻿            _healthEntity.OnHealthChanged += health => OnHealthChanged?.Invoke(health);
﻿        }
﻿
﻿        public event Action<int> OnHealthChanged;
﻿        // public event Action<Vector3> OnHit; // Remove OnHit event
﻿        
﻿        public int CurrentHealth => _healthEntity.CurrentHealth;
﻿        public int MaxHealth => _healthEntity.MaxHealth;
﻿        
﻿        public void Update() { }
﻿
﻿        public void TakeDamage(int damage, Vector3 attackDirection)
﻿        {
﻿            _healthEntity.TakeDamage(damage);
﻿            // OnHit?.Invoke(attackDirection); // Remove OnHit event invocation
﻿            _playerImpactUseCase.GenerateImpact(attackDirection); // Call PlayerImpactUseCase
﻿        }
﻿        
﻿        public void OnMoveInput(Vector2 input, float deltaTime)
﻿        {
﻿            _movePlayerUseCase.Move(input, deltaTime);
﻿        }
﻿
﻿        public void OnRunInput(bool isRunning)
﻿        {
﻿            _movePlayerUseCase.SetRunning(isRunning);
﻿        }
﻿
﻿        public void OnSlideInput(bool isSliding)
﻿        {
﻿            _movePlayerUseCase.Slide(isSliding);
﻿        }
﻿        public void OnLookInput(Vector2 input)
﻿        {
﻿            _movePlayerUseCase.Look(input);
﻿        }
﻿
﻿        private readonly MovePlayerUseCase _movePlayerUseCase;
﻿        private readonly HealthEntity _healthEntity;
﻿    }
﻿}
﻿