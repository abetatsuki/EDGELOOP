using Develop.Gun.Interface;
using System;
using UnityEngine;
namespace Develop.Gun
{
    public class GunFire 
    {
        public event Action OnFire;
        public event Action<Transform,Vector3,Vector3> OnHit;
        public void Fire(Vector3 origin, Vector3 direction, float maxDistance, LayerMask layerMask)
        {
            OnFire?.Invoke();

            if (Physics.Raycast(origin, direction, out RaycastHit hit, maxDistance))
            {
                Debug.Log($"Hit: {hit.collider.name} at {hit.point}");
                // TODO: 敵にダメージを与える処理や、着弾エフェクトの生成
                OnHit?.Invoke(hit.transform,hit.point,hit.normal);
                if (hit.collider.TryGetComponent<IDamageable>(out var damageable))
                {
                    DamageUtility.ApplyDamage(damageable, 10); 
                }
            }
            else
            {
                Debug.Log("No hit.");
            }
        }
    }
}