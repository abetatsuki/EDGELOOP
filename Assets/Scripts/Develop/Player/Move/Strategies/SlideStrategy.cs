using Develop.Interface;
using Develop.Player.Move.Strategies;
using UnityEngine;

public class SlideStrategy : IMovementStrategy
{
    public SlideStrategy(float slideDamping, float endSpeed)
    {
        _slideDamping = slideDamping;
        _endSpeed = endSpeed;
    }
    public void Move(IMovableBody body, Vector2 input, float deltaTime)
    {

    }


    private readonly float _slideDamping;
    private readonly float _endSpeed;
}
