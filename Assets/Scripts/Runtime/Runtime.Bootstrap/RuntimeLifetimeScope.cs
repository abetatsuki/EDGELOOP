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
            // Core
            builder.Register<CharacterEntity>(Lifetime.Singleton);

            // Usecases (contract only)
            builder.Register<Movement>(Lifetime.Singleton)
                .As<IJumpInputPort>()
                .As<IMoveInputPort>()
                .As<IDashInputPort>();
            builder.Register<Environment>(Lifetime.Singleton).As<IEnvironmentInputPort>();

            // Presentation components
            builder.RegisterComponentInHierarchy<RigidBodyAdaptor>()
                .As<IJumpPhysicsOutput>()
                .As<IMovePhysicsOutput>()
                .As<IDashPhysicsOutput>();
            builder.RegisterComponentInHierarchy<InputBuffer>();
            builder.RegisterComponentInHierarchy<EnviromentAdaptor>();
        }
    }
}


