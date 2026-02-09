using Develop.Interface;
using Develop.Player.Entity;
using Develop.Player.Move.Strategies;
using UnityEngine;

namespace Develop.Player.Usecase
{
    public class MovePlayerUseCase
    {
        private readonly PlayerEntity _playerEntity;
        private readonly IMovableBody _body;
        private readonly WalkStrategy _walkStrategy;
        private readonly RunStrategy _runStrategy;
        private readonly SlideStrategy _slideStrategy;
        private readonly ILook _look;

        private IMovementStrategy _currentStrategy;
        private IMovementStrategy _previousStrategy; // 追加

        public MovePlayerUseCase(
            PlayerEntity playerEntity,
            IMovableBody body,
            WalkStrategy walkStrategy, // 具体的な型にする
            RunStrategy runStrategy,   // 具体的な型にする
            SlideStrategy slideStrategy, // 具体的な型にする
            ILook look)
        {
            _playerEntity = playerEntity;
            _body = body;
            _walkStrategy = walkStrategy;
            _runStrategy = runStrategy;
            _slideStrategy = slideStrategy;
            _look = look;
            _currentStrategy = _walkStrategy; // 初期状態
            _previousStrategy = _walkStrategy; // 初期状態
            _currentStrategy.Enter(_body, _playerEntity); // 初期戦略のEnterを呼ぶ
        }

        public void Move(Vector2 input, float deltaTime)
        {
            _currentStrategy.Execute(_body, input, deltaTime);
        }

        public void Look(Vector2 input)
        {
            _look.Look(input);
        }

        public void Slide(bool isSliding)
        {
            if (isSliding)
            {
                if (!_playerEntity.IsSliding && _playerEntity.CanSliding()) // 現在スライディング中でなく、スライディング可能なら
                {
                    _previousStrategy = _currentStrategy; // 現在の戦略を保存
                    _currentStrategy.Exit(_body, _playerEntity);
                    _currentStrategy = _slideStrategy;
                    _currentStrategy.Enter(_body, _playerEntity);
                }
            }
            else // !isSliding
            {
                if (_playerEntity.IsSliding) // スライディング中であれば終了
                {
                    _currentStrategy.Exit(_body, _playerEntity);
                    _currentStrategy = _previousStrategy; // 前の戦略に戻す
                    _currentStrategy.Enter(_body, _playerEntity);
                }
            }
        }

        public void SetRunning(bool isRunning)
        {
            if (_playerEntity.IsSliding) return; // スライディング中は実行しない

            IMovementStrategy newStrategy = isRunning ? _runStrategy : _walkStrategy;

            if (_currentStrategy != newStrategy)
            {
                _previousStrategy = _currentStrategy; // 現在の戦略を保存
                _currentStrategy.Exit(_body, _playerEntity);
                _currentStrategy = newStrategy;
                _currentStrategy.Enter(_body, _playerEntity);
            }
        }
        
        // 古いMovePlayerUseCaseのコードは削除
        // public void Move(Vector2 input, float deltaTime)
        // {
        //     if (input == Vector2.zero)
        //     {
        //         //ここにIdle処理を描く。
        //     }
        //     else if (_playerEntity.CanSliding())
        //     {
        //         _current.Move(_body, Vector2.zero, deltaTime);
        //     }
        //     else if (_playerEntity.CanMove())
        //     {
        //         _current.Move(_body, input, deltaTime);
        //     }
        // }
        // public void Slide(bool isSliding)
        // {
        //     if (isSliding)
        //     {
        //         _playerEntity.StartSliding();
        //         _current = _slide;
        //     }
        //     else if (!isSliding)
        //     {
        //         _playerEntity.StopSliding();
        //         _current = _walk;
        //     }
        // }
    }
}

