using Develop.Interface;
using UnityEngine;
namespace Develop
{
    public static class DamageUtility
    {
        public static void ApplyDamage(IDamageable damageable, int damage)
        {
            damageable?.TakeDamage(damage, Vector3.zero);
        }

    }

}
