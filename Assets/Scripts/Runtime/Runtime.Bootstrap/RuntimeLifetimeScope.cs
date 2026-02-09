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
            // Core
            builder.Register<CharacterEntity>(Lifetime.Singleton);

            // Usecases (contract only)
            builder.Register<Movement>(Lifetime.Singleton).As<IJumpInputPort>().As<ICharacterConfigPort>();
            builder.Register<Enviroment>(Lifetime.Singleton).As<IApplyEnvironmentState>();

            // Presentation components
            builder.RegisterComponentInHierarchy<RigidBodyAdaptor>().As<IJumpPhysicsOutput>();
            builder.RegisterComponentInHierarchy<InputBuffer>();
            builder.RegisterComponentInHierarchy<EnviromentAdaptor>();
            builder.RegisterComponentInHierarchy<CharacterDataAdaptor>();
        }
    }
}
