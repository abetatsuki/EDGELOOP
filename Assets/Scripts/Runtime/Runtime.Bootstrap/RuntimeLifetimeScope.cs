using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Runtime
{
    public class RuntimeLifetimeScope : LifetimeScope
    {
        [SerializeField] private CharacterConfig _characterConfig;
        [SerializeField] private MoveSpeedUiAdaptor _moveSpeedUiAdaptor;
        [SerializeField] private MoveSpeedUiView[] _moveSpeedUiViews;
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(_characterConfig);
            var characterConfigData = new CharacterConfigData(
                _characterConfig.JumpPower,
                _characterConfig.MoveSpeed,
                _characterConfig.DashPower,
                _characterConfig.RunMinRatio,
                _characterConfig.RunScrollStep,
                _characterConfig.WallRunForce,
                _characterConfig.WallClimbSpeed,
                _characterConfig.MaxWallRunTime,
                _characterConfig.WallStickForce
            );
            builder.RegisterInstance(characterConfigData);
            var cameraConfigData = new CameraConfigData(
                _characterConfig.LookSpeed,
                _characterConfig.MaxPitch
            );
            builder.RegisterInstance(cameraConfigData);
            // Core
            builder.Register<CharacterEntity>(Lifetime.Singleton);

            // Usecases (contract only)
            builder.Register<Movement>(Lifetime.Singleton)
                .As<IJumpInputPort>()
                .As<IMoveInputPort>()
                .As<IDashInputPort>()
                .As<ISlideInputPort>()
                .As<IRunSpeedInputPort>()
                .As<IControlRotationOutput>();
            builder.Register<CameraLook>(Lifetime.Singleton)
                .As<ILookInputPort>();
            builder.Register<Environment>(Lifetime.Singleton).As<IEnvironmentInputPort>();
            builder.Register<WallRun>(Lifetime.Singleton)
                .As<IWallRunInputPort>()
                .As<IWallSenseInputPort>()
                .As<IWallRunTickInputPort>()
                .As<IControlRotationOutput>();

            // Presentation components
            builder.RegisterComponentInHierarchy<RigidBodyAdaptor>()
                .As<IJumpPhysicsOutput>()
                .As<IMovePhysicsOutput>()
                .As<IDashPhysicsOutput>()
                .As<ISlideMotionOutput>()
                .As<IWallRunPhysicsOutput>();
            builder.RegisterComponentInHierarchy<FpsCameraAdaptor>()
                .As<ICameraRotationOutput>();
            builder.RegisterComponentInHierarchy<InputBuffer>();
            builder.RegisterComponentInHierarchy<EnviromentAdaptor>();
            builder.RegisterComponentInHierarchy<WallDetecter>();
            builder.RegisterComponentInHierarchy<WallRunDriver>();

            if (_moveSpeedUiAdaptor != null)
            {
                builder.RegisterComponent(_moveSpeedUiAdaptor)
                    .As<IMoveSpeedOutput>();
            }

            if (_moveSpeedUiViews != null)
            {
                for (int i = 0; i < _moveSpeedUiViews.Length; i++)
                {
                    MoveSpeedUiView view = _moveSpeedUiViews[i];
                    if (view == null)
                    {
                        continue;
                    }
                    builder.RegisterComponent(view)
                        .As<IMoveSpeedUiInput>();
                }
            }
        }
    }
}
