namespace Develop.Gun
{
    public class GunPresenter : IGunRequest
    {
        public GunPresenter(IWeapon weapon)
        {
            _useCase = weapon;
        }

        public void OnFireRequest()
        {
            _useCase.TryFire();
        }
        public void OnAimRequest(bool isAim)
        {
            _isAiming = isAim;
        }
        public void Update()
        {
            _useCase.TryAim(_isAiming);
        }

        private bool _isAiming;
        private IWeapon _useCase;
    }

}