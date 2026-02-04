using UnityEngine;

namespace Develop.Interface
{
    public interface IDamageable
    {
        void TakeDamage(int damage, Vector3 attackDirection);
    }
}
