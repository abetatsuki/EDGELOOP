using UnityEngine;

namespace Develop.Gun
{
    public class GunPresenter : IGunRequest
    {
        public GunPresenter(IWeapon weapon)
        {
            _useCase = weapon;
        }

        public void OnFireRequest(bool isFiring)
        {
            _isFiring = isFiring;
        }
        public void OnAimRequest(bool isAim)
        {
            _isAiming = isAim;
        }
        
        public void OnReloadRequest()
        {
            _useCase.TryReload();
        }
        
        public void Update()
        {
            if (_isFiring)
            {
                _useCase.TryFire();
            }
            _useCase.TryAim(_isAiming);
            _useCase.Update();
        }

        private bool _isFiring;
        private bool _isAiming;
        private IWeapon _useCase;
    }

}