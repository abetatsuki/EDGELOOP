using UnityEngine;

namespace Runtime
{
    [CreateAssetMenu(fileName = "CharacterConfig", menuName = "Runtime/CharacterConfig")]
    public class CharacterConfig : ScriptableObject
    {
      [SerializeField] private float _jumpPower = 5f;
      public float JumpPower => _jumpPower;
    }
}
