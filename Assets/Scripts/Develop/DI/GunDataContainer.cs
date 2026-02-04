using Develop.Gun;
using Develop.Gun.Interface;
using Develop.Interface;

namespace Develop.DI
{
    public class GunDataContainer
    {
        public GunPresenter GunPresenter { get; private set; }

        public void Init(GunConfig gunConfig, IGunView gunView)
        {
            var gunEntity = new GunEntity(gunConfig);
            var gunFire = new GunFire();
            var gunEffect = new GunEffect(gunView);
            var gunAim = new GunAim(gunView, gunConfig.AimToSpeed);
            var gunAnim = new GunAnimController(gunView.GunAnimator);

            var gunLookInputSource = new GunLookInputSource(); 

            var gun = new GunUseCase(gunEntity, gunFire, gunConfig, gunView, gunEffect, gunAim, gunAnim, gunLookInputSource);
            gun.Init();
            GunPresenter = new GunPresenter(gun, gunLookInputSource);
        }
    }
}
