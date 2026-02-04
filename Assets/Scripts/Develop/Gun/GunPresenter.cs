using Develop.Interface; // Added for ILookInputSource
using UnityEngine;

namespace Develop.Gun
{
    public class GunPresenter : IGunRequest
    {
        private readonly IWeapon _useCase;
        private readonly GunLookInputSource _lookInputSource; // New field
        private bool _isFiring;
        private bool _isAiming;

        public GunPresenter(IWeapon weapon, GunLookInputSource lookInputSource) // Modified constructor
        {
            _useCase = weapon;
            _lookInputSource = lookInputSource; // Store the input source
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
        public void OnLookInput(Vector2 input)
        {
            _lookInputSource.SetLookInput(input); // Use the input source to set the input
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
    }
}