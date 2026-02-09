using Develop.Interface;
using Develop.Player;
using Develop.Player.Camera;
using Develop.Player.Entity;
using Develop.Player.Move.Strategies;
using Develop.Player.Usecase;
using Unity.Cinemachine; 
using UnityEngine; 

namespace Develop.DI
{
    public class PlayerDataContainer
    {
        public PlayerPresenter PlayerPresenter { get; private set; }
        public HealthEntity HealthEntity { get; private set; }
        public PlayerImpactUseCase PlayerImpactUseCase { get; private set; } 
        public JumpPlayerUseCase JumpPlayerUseCase { get; private set; } 

        public void Init(IMovableBody body, PlayerConfig config, CinemachineImpulseSource impulseSource, Transform playerTransform, Transform gunImpactTransform)
        {
            HealthEntity = new HealthEntity(100);
            var playerEntity = new PlayerEntity(); // PlayerEntityはここで生成

            var cameralook = new CameraLook(body, config.LookSpeed, config.MaxAngle);
            var walkStrategy = new WalkStrategy(config.WalkSpeed);
            var runStrategy = new RunStrategy(config.RunSpeed);
            var slideStrategy = new SlideStrategy(body, playerEntity, config); // SlideStrategyの生成
            
            var movePlayer = new MovePlayerUseCase(playerEntity, body, walkStrategy, runStrategy, slideStrategy, cameralook);
            
            PlayerImpactUseCase = new PlayerImpactUseCase(
                impulseSource, 
                playerTransform, 
                config.ImpactStrength, 
                config.DiagonalThreshold,
                gunImpactTransform, 
                config.GunImpactForce, 
                config.GunImpactOffset); 

            JumpPlayerUseCase = new JumpPlayerUseCase(body, playerEntity, config); 
            
            PlayerPresenter = new PlayerPresenter(movePlayer, HealthEntity, PlayerImpactUseCase, JumpPlayerUseCase, playerEntity, config); // playerEntityとconfigも渡す
        }
    }
}
