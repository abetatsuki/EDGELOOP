using UnityEngine;
namespace Develop.Gun
{
    public class GunEntity
    {
   
        public GunEntity(GunConfig config)
        {
            _currentAmmo = config.Ammo;
        }

        public bool CanFire()
        {
            return _currentAmmo > 0 && Time.time >= _nextFireTime;
        }

        public void RecordFire(float fireRate)
        {
            if (_currentAmmo > 0)
            {
                _currentAmmo--;
                _nextFireTime = Time.time + 1f / fireRate;
            }
        }
        
        public void SetAiming(bool isAiming)
        {
            _isAiming = isAiming;
        }
        public bool IsAiming()
        {
            return _isAiming;
        }

        private float _nextFireTime;
        private int _currentAmmo;
        private bool _isAiming;
    }
}