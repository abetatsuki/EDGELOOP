using UnityEngine;

namespace Runtime
{
    [CreateAssetMenu(fileName = "CharacterConfig", menuName = "Runtime/CharacterConfig")]
    public class CharacterConfig : ScriptableObject
    {
      [SerializeField] private float _jumpPower = 5f;
      [SerializeField] private float _moveSpeed = 3f;
      [SerializeField] private float _dashPower = 8f;
      [SerializeField] private float _lookSpeed = 1f;
      [SerializeField] private float _maxPitch = 80f;
      [SerializeField] private float _runMinRatio = 0.25f;
      [SerializeField] private float _runScrollStep = 0.1f;
      [SerializeField] private float _wallRunForce = 15f;
      [SerializeField] private float _wallClimbSpeed = 2f;
      [SerializeField] private float _maxWallRunTime = 1.5f;
      [SerializeField] private float _wallStickForce = 100f;

      public float JumpPower => _jumpPower;
      public float MoveSpeed => _moveSpeed;
      public float DashPower => _dashPower;
      public float LookSpeed => _lookSpeed;
      public float MaxPitch => _maxPitch;
      public float RunMinRatio => _runMinRatio;
      public float RunScrollStep => _runScrollStep;
      public float WallRunForce => _wallRunForce;
      public float WallClimbSpeed => _wallClimbSpeed;
      public float MaxWallRunTime => _maxWallRunTime;
      public float WallStickForce => _wallStickForce;
    }
}
