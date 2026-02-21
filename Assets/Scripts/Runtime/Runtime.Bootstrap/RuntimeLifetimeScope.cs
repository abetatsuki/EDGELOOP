using System.Collections.Generic;
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
            builder.Register<PlayerRuntimeFactory>(Lifetime.Singleton)
                .As<IPlayerRuntimeFactory>();
            builder.RegisterBuildCallback(container =>
            {
                RuntimeServiceRegistry.PlayerRuntimeFactory = container.Resolve<IPlayerRuntimeFactory>();
                if (container.TryResolve<IReadOnlyList<IMoveSpeedUiInput>>(out var uiInputs))
                {
                    RuntimeServiceRegistry.MoveSpeedUiInputs = uiInputs;
                }
                else
                {
                    RuntimeServiceRegistry.MoveSpeedUiInputs = null;
                }
            });

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
