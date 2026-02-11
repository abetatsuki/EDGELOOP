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
      [SerializeField] private float _wallRunSpeed = 6f;
      [SerializeField] private float _wallRunDuration = 1.0f;
      public float JumpPower => _jumpPower;
      public float MoveSpeed => _moveSpeed;
      public float DashPower => _dashPower;
      public float LookSpeed => _lookSpeed;
      public float MaxPitch => _maxPitch;
      public float RunMinRatio => _runMinRatio;
      public float RunScrollStep => _runScrollStep;
      public float WallRunSpeed => _wallRunSpeed;
      public float WallRunDuration => _wallRunDuration;
    }
}
