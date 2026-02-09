using Develop.Gun.Interface;
using Develop.Interface;
using Develop.Player.Entity;
using UnityEngine;

public class DummyPlayer : MonoBehaviour ,IDamageable
{
    private readonly HealthEntity _healthEntity = new HealthEntity(100);
    public void TakeDamage(int damage, Vector3 attackDirection)
    {
        _healthEntity.TakeDamage(damage);
    }
}
