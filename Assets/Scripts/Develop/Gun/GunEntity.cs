using UnityEngine;
namespace Develop.Gun
{
    public class GunEntity
    {
   
        public GunEntity(GunConfig config)
        {
            _currentAmmo = config.MaxAmmo;
            _maxAmmo = config.MaxAmmo;
            _fireRate = 1/config.FireRate;

        }

        public bool CanFire()
        {
            Debug.Log($"{_currentAmmo} + {Time.time >=_nextFireTime} +{ _isReloading}");
            return _currentAmmo > 0 && Time.time >= _nextFireTime && !_isReloading;
        }
        public bool CanReload()
        {
            return !_isReloading && _currentAmmo < _maxAmmo;
        }
        public void RecordFire(float currentTime)
        {
            if (_currentAmmo > 0)
            {
                _currentAmmo--;
                _nextFireTime = currentTime + _fireRate;
            }
        }
        
        public void Reload()
        {
            _currentAmmo = _maxAmmo;
            Debug.Log("ƒŠƒ[ƒhŠ®—¹");
        }
        public void SetReloading(bool isReloading)
        {
            _isReloading = isReloading;
        }
        public void SetAiming(bool isAiming)
        {
            _isAiming = isAiming;
        }
        public bool IsAiming()
        {
            return _isAiming;
        }
        public bool IsReloading()
        {
            return _isReloading;
        }
        
        public int CurrentAmmo => _currentAmmo;
        public int MaxAmmo => _maxAmmo;
        
        private bool _isReloading;
        private float _nextFireTime;
        private float _fireRate;
        private int _currentAmmo;
        private int _maxAmmo;
        private bool _isAiming;
    }
}