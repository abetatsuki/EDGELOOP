using UnityEngine;

namespace Runtime
{
    [CreateAssetMenu(fileName = "CharacterConfig", menuName = "Runtime/CharacterConfig")]
    public class CharacterConfig : ScriptableObject
    {
      [SerializeField] private float _jumpPower = 5f;
      [SerializeField] private float _moveSpeed = 3f;
      [SerializeField] private float _dashPower = 8f;
      public float JumpPower => _jumpPower;
      public float MoveSpeed => _moveSpeed;
      public float DashPower => _dashPower;
    }
}
