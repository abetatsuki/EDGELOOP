using System;
﻿﻿using Develop.Interface;
﻿﻿using Develop.Player.Entity;
﻿﻿using Develop.Player.Usecase;
﻿﻿using UnityEngine;
﻿﻿
﻿﻿namespace Develop.Player
﻿﻿{
﻿﻿    public class PlayerPresenter : IPlayerInputPort, IPlayerUpdatable
﻿﻿    {
﻿﻿        private readonly PlayerImpactUseCase _playerImpactUseCase;
﻿﻿        private readonly JumpPlayerUseCase _jumpPlayerUseCase;
﻿﻿        private readonly PlayerEntity _playerEntity; // 追加
﻿﻿        private readonly PlayerConfig _config; // 追加
﻿﻿
﻿﻿        public PlayerPresenter(MovePlayerUseCase move, HealthEntity healthEntity, PlayerImpactUseCase playerImpactUseCase, JumpPlayerUseCase jumpPlayerUseCase, PlayerEntity playerEntity, PlayerConfig config) // 引数追加
﻿﻿        {
﻿﻿            _movePlayerUseCase = move;
﻿﻿            _healthEntity = healthEntity;
﻿﻿            _playerImpactUseCase = playerImpactUseCase;
﻿﻿            _jumpPlayerUseCase = jumpPlayerUseCase;
﻿﻿            _playerEntity = playerEntity; // 初期化
﻿﻿            _config = config; // 初期化
﻿﻿
﻿﻿            _healthEntity.OnHealthChanged += health => OnHealthChanged?.Invoke(health);
﻿﻿        }
﻿﻿
﻿﻿        public event Action<int> OnHealthChanged;
﻿﻿        
﻿﻿        public int CurrentHealth => _healthEntity.CurrentHealth;
﻿﻿        public int MaxHealth => _healthEntity.MaxHealth;
﻿
﻿﻿        public PlayerEntity PlayerEntity => _playerEntity; // 追加
﻿﻿        public PlayerConfig PlayerConfig => _config; // 追加
﻿
﻿﻿        public float SlideColliderHeight => _config.SlideColliderHeight; // 追加
﻿﻿        public float SlideColliderCenterY => _config.SlideColliderCenterY; // 追加
﻿﻿        
﻿﻿        public void Update()
﻿﻿        {
﻿﻿            _jumpPlayerUseCase.UpdateGroundStatus();
﻿﻿        }
﻿﻿
﻿﻿        public void TakeDamage(int damage, Vector3 attackDirection)
﻿﻿        {
﻿﻿            _healthEntity.TakeDamage(damage);
﻿﻿            _playerImpactUseCase.GenerateImpact(attackDirection);
﻿﻿        }
﻿﻿        
﻿﻿        public void OnMoveInput(Vector2 input, float deltaTime)
﻿﻿        {
﻿﻿            _movePlayerUseCase.Move(input, deltaTime);
﻿﻿        }
﻿﻿
﻿﻿        public void OnRunInput(bool isRunning)
﻿﻿        {
﻿﻿            _movePlayerUseCase.SetRunning(isRunning);
﻿﻿        }
﻿
﻿﻿        // OnSlideInputは後でMovePlayerUseCaseにスライディング開始を指示するために使う
﻿﻿        public void OnSlideInput(bool isSliding)
﻿﻿        {
﻿﻿            _movePlayerUseCase.Slide(isSliding); // MovePlayerUseCaseに処理を委譲
﻿﻿        }
﻿
﻿﻿        public void OnLookInput(Vector2 input)
﻿﻿        {
﻿﻿            _movePlayerUseCase.Look(input);
﻿﻿        }
﻿﻿
﻿﻿        public void OnJumpInput()
﻿﻿        {
﻿﻿            _jumpPlayerUseCase.Jump();
﻿﻿        }
﻿﻿
﻿﻿        private readonly MovePlayerUseCase _movePlayerUseCase;
﻿﻿        private readonly HealthEntity _healthEntity;
﻿﻿    }
﻿﻿}
﻿
﻿