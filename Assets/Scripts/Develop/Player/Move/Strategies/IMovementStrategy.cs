using Develop.Interface;
using Develop.Player.Entity; // PlayerEntityのために追加
using UnityEngine;

namespace Develop.Player.Move.Strategies
{
    public interface IMovementStrategy
    {
        void Enter(IMovableBody body, PlayerEntity playerEntity); // 追加
        void Exit(IMovableBody body, PlayerEntity playerEntity); // 追加
        void Execute(IMovableBody body, Vector2 input, float deltaTime); // 既存のMoveを置き換え
    }
}
