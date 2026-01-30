using UnityEngine;
namespace Develop.Gun
{
    public class GunEntity
    {
        public GunEntity(int ammo)
        {
            _currentAmmo = ammo;
        }
        public bool IsFiring()
        {
            return  _currentAmmo > 0;
        }
        public void SetCurrentAmmo(int ammo)
        {
            _currentAmmo = ammo;
        }
        public void DecreaseAmmo()
        {
            if (_currentAmmo > 0)
            {
                _currentAmmo--;
            }
        }
        public void SetAiming(bool isAiming)
        {
            _isAiming = isAiming;
        }
        private int _currentAmmo;
        private bool _isAiming;


    }
}