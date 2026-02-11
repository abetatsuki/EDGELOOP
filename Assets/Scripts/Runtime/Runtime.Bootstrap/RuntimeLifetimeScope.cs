using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Runtime
{
    public class RuntimeLifetimeScope : LifetimeScope
    {
        [SerializeField] private CharacterConfig _characterConfig;
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(_characterConfig);
            var characterConfigData = new CharacterConfigData(
                _characterConfig.JumpPower,
                _characterConfig.MoveSpeed,
                _characterConfig.DashPower
            );
            builder.RegisterInstance(characterConfigData);
            var cameraConfigData = new CameraConfigData(
                _characterConfig.LookSpeed,
                _characterConfig.MaxPitch
            );
            builder.RegisterInstance(cameraConfigData);
            // Core
            builder.Register<CharacterEntity>(Lifetime.Singleton);
            builder.Register<ControlRotationState>(Lifetime.Singleton)
                .As<IControlRotationReader>()
                .As<IControlRotationWriter>();

            // Usecases (contract only)
            builder.Register<Movement>(Lifetime.Singleton)
                .As<IJumpInputPort>()
                .As<IMoveInputPort>()
                .As<IDashInputPort>()
                .As<ISlideInputPort>();
            builder.Register<Lookment>(Lifetime.Singleton)
                .As<ILookInputPort>();
            builder.Register<Environment>(Lifetime.Singleton).As<IEnvironmentInputPort>();

            // Presentation components
            builder.RegisterComponentInHierarchy<RigidBodyAdaptor>()
                .As<IJumpPhysicsOutput>()
                .As<IMovePhysicsOutput>()
                .As<IDashPhysicsOutput>()
                .As<ISlideMotionOutput>();
            builder.RegisterComponentInHierarchy<FpsCameraAdaptor>()
                .As<ICameraRotationOutput>();
            builder.RegisterComponentInHierarchy<InputBuffer>();
            builder.RegisterComponentInHierarchy<EnviromentAdaptor>();
        }
    }
}

