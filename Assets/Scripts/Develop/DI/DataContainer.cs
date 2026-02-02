using Develop.Gun;
using Develop.Gun.Interface;
using Develop.Interface;
using Develop.Player;
using Develop.Player.Camera;
using Develop.Player.Entity;
using Develop.Player.Move.Strategies;
using Develop.Player.Usecase;
using Develop.UI;

namespace Develop.DI
{
    public class DataContainer
    {
        public PlayerPresenter PlayerPresenter { get; private set; }

        public GunPresenter GunPresenter { get; private set; }
        public InputBuffer InputBuffer { get; private set; }

        public void Init(IMovableBody body, PlayerConfig config,IPlayer player,GunConfig gunConfig,IGunView gunView)
        {
            var playerEntity = new PlayerEntity();
            var cameralook = new CameraLook(body,config.LookSpeed,config.MaxAngle);
            var walkStrategy = new WalkStrategy(config.WalkSpeed);
            var runStrategy = new RunStrategy(config.RunSpeed);
            var slideStrategy = new SlideStrategy(config.DecelerationRate,config.EndSpeed,config.GroundLayer,config.GroundCheckDistance);
            var movePlayer = new MovePlayerUseCase(playerEntity,body,walkStrategy,runStrategy,slideStrategy,cameralook);
            PlayerPresenter = new PlayerPresenter(movePlayer);

            var gunEntity = new GunEntity(gunConfig);
            var gunFire = new GunFire();
            var gunEffect = new GunEffect(gunView);
            var gunAim = new GunAim(gunView,gunConfig.AimToSpeed);
            var gunAnim = new GunAnimController(gunView.GunAnimator);
            var gun = new GunUseCase(gunEntity,gunFire,gunConfig,gunView,gunEffect,gunAim,gunAnim);
            gun.Init();
            GunPresenter = new GunPresenter(gun);

            InputBuffer = new InputBuffer(player,PlayerPresenter,GunPresenter);
        }
    }
}