using UnityEngine;

namespace Runtime
{
    [CreateAssetMenu(fileName = "CharacterConfig", menuName = "Runtime/CharacterConfig")]
    public class CharacterConfig : ScriptableObject
    {
      [SerializeField] private float _jumpPower = 5f;
      [SerializeField] private float _moveSpeed = 3f;
      public float JumpPower => _jumpPower;
      public float MoveSpeed => _moveSpeed;
    }
}
