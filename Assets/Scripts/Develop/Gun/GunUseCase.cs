using UnityEngine;
namespace Develop.Gun
{
    public class GunUseCase : IWeapon
    {

        public GunUseCase(GunEntity entity,
            GunFire fire)
        {
            _entity = entity;
            _fire = fire;
        }
        public void TryFire()
        {
            if(_entity.IsFiring())
            {
                _entity.DecreaseAmmo();
                _fire.Fire();
            }
            else
            {
                Debug.Log("弾の発射条件を満たしていません");
            }
        }
        private GunEntity _entity;
        private GunFire _fire;
    }
}
