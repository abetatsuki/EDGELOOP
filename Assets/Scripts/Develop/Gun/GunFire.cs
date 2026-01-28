using UnityEngine;
namespace Develop.Gun
{
    public class GunFire 
    {
        public void Fire(Vector3 origin, Vector3 direction, float maxDistance, LayerMask layerMask)
        {
            if (Physics.Raycast(origin, direction, out RaycastHit hit, maxDistance, layerMask))
            {
                Debug.Log($"Hit: {hit.collider.name} at {hit.point}");
                // TODO: 敵にダメージを与える処理や、着弾エフェクトの生成
            }
            else
            {
                Debug.Log("No hit.");
            }
        }
    }
}