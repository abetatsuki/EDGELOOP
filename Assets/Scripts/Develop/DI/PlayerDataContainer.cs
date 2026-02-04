using Develop.Interface;
using Develop.Player;
using Develop.Player.Camera;
using Develop.Player.Entity;
using Develop.Player.Move.Strategies;
using Develop.Player.Usecase;
using Unity.Cinemachine; // Add Cinemachine using
using UnityEngine; // Add UnityEngine using for Transform

namespace Develop.DI
{
    public class PlayerDataContainer
    {
        public PlayerPresenter PlayerPresenter { get; private set; }
        public HealthEntity HealthEntity { get; private set; }
        public PlayerImpactUseCase PlayerImpactUseCase { get; private set; } // Add PlayerImpactUseCase property

        public void Init(IMovableBody body, PlayerConfig config, CinemachineImpulseSource impulseSource, Transform playerTransform)
        {
            HealthEntity = new HealthEntity(100);
            var playerEntity = new PlayerEntity();
            var cameralook = new CameraLook(body, config.LookSpeed, config.MaxAngle);
            var walkStrategy = new WalkStrategy(config.WalkSpeed);
            var runStrategy = new RunStrategy(config.RunSpeed);
            var slideStrategy = new SlideStrategy(config.DecelerationRate, config.EndSpeed, config.GroundLayer, config.GroundCheckDistance);
            var movePlayer = new MovePlayerUseCase(playerEntity, body, walkStrategy, runStrategy, slideStrategy, cameralook);
            
            PlayerImpactUseCase = new PlayerImpactUseCase(impulseSource, playerTransform, config.ImpactStrength, config.DiagonalThreshold); // Instantiate PlayerImpactUseCase
            
            PlayerPresenter = new PlayerPresenter(movePlayer, HealthEntity, PlayerImpactUseCase); // Pass PlayerImpactUseCase to PlayerPresenter
        }
    }
}
