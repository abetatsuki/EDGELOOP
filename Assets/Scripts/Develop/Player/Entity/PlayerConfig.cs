using UnityEngine;

namespace Develop.Player.Entity
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Develop/PlayerConfig")]
    public class PlayerConfig : ScriptableObject
    {
        public float WalkSpeed => _walkSpeed;
        public float RunSpeed => _runSpeed;
        public float Damping => _damping;
        public float SlidingEnd => _slidingEnd;

        [Header("MovementState")]
        [SerializeField] private float _walkSpeed;
        [SerializeField] private float _runSpeed;
        [SerializeField] private float _damping;
        [SerializeField] private float _slidingEnd;
        
    }
}