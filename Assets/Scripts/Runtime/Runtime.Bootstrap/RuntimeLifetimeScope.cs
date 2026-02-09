using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Runtime
{
    public class RuntimeLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            // Core
            builder.Register<CharacterEntity>(Lifetime.Singleton);

            // Usecases (contract only)
            builder.Register<Movement>(Lifetime.Singleton).As<IJumpInputPort>();
            builder.Register<Enviroment>(Lifetime.Singleton).As<IApplyEnvironmentState>();

            // Presentation components
            builder.RegisterComponentInHierarchy<RigidBodyAdaptor>().As<IJumpPhysicsOutput>();
            builder.RegisterComponentInHierarchy<InputBuffer>();
            builder.RegisterComponentInHierarchy<EnviromentAdaptor>();
        }
    }
}
