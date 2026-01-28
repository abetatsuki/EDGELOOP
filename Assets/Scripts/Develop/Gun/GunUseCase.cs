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
                Debug.Log("’e‚Ì”­ËğŒ‚ğ–‚½‚µ‚Ä‚¢‚Ü‚¹‚ñ");
            }
        }
        private GunEntity _entity;
        private GunFire _fire;
    }
}
