using UnityEngine;

namespace Develop.Player.Entity
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Develop/PlayerConfig")]
    public class PlayerConfig : ScriptableObject
    {
        public float WalkSpeed = 3f;
        public float RunSpeed = 6f;
    }
}