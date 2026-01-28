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

        
        private IWeapon _useCase;
    }

}