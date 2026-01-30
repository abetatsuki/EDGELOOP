using System;
using UnityEngine;
namespace Develop.Gun
{
    public class GunFire 
    {
        public event Action<Vector3> OnFire;
        public event Action<Vector3> OnHit;
        public void Fire(Vector3 origin, Vector3 direction, float maxDistance, LayerMask layerMask)
        {
            OnFire?.Invoke(origin);

            if (Physics.Raycast(origin, direction, out RaycastHit hit, maxDistance))
            {
                Debug.Log($"Hit: {hit.collider.name} at {hit.point}");
                // TODO: 敵にダメージを与える処理や、着弾エフェクトの生成
                OnHit?.Invoke(hit.point);
            }
            else
            {
                Debug.Log("No hit.");
            }
        }
    }
}