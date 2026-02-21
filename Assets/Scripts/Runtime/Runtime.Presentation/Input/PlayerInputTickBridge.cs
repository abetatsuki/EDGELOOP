using System;
using System.Collections.Generic;
using Fusion;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;
using VContainer.Unity;

namespace Runtime
{
    public class PlayerInputTickBridge : NetworkBehaviour
    {
        private IJumpInputPort _jumpPort;
        private IMoveInputPort _movePort;
        private IDashInputPort _dashPort;
        private ISlideInputPort _slidePort;
        private IRunSpeedInputPort _runSpeedPort;
        private ILookInputPort _lookPort;
        private IWallRunInputPort _wallRunInputPort;
        private IWallSenseInputPort _wallSenseInputPort;
        private IWallRunTickInputPort _wallRunTickInputPort;
        private bool _isResolved;

        public override void Spawned()
        {
            ResolvePorts();
            SetLocalInputEnabled(Object.HasInputAuthority);

            if (!Object.HasInputAuthority)
            {
                return;
            }

            FusionInputFromBuffer provider = Runner != null ? Runner.GetComponent<FusionInputFromBuffer>() : null;
            InputBuffer buffer = GetComponent<InputBuffer>();
            if (provider != null && buffer != null)
            {
                provider.SetBuffer(Object.InputAuthority, buffer);
            }
        }

        public override void Despawned(NetworkRunner runner, bool hasState)
        {
            SetLocalInputEnabled(false);

            if (!Object.HasInputAuthority)
            {
                return;
            }

            FusionInputFromBuffer provider = runner != null ? runner.GetComponent<FusionInputFromBuffer>() : null;
            if (provider != null)
            {
                provider.SetBuffer(Object.InputAuthority, null);
            }
        }

        public override void FixedUpdateNetwork()
        {
            if (!Object.HasStateAuthority && !Object.HasInputAuthority)
            {
                return;
            }

            if (!_isResolved)
            {
                ResolvePorts();
                if (!_isResolved)
                {
                    return;
                }
            }

            if (GetInput(out NetInput ni))
            {
                if (Object.HasStateAuthority)
                {
                    _movePort.Handle(new MoveInputData { X = ni.Move.x, Y = ni.Move.y });
                    _lookPort.Handle(new LookInputData { X = ni.Look.x, Y = ni.Look.y });

                    _jumpPort.Handle(new JumpInputData { IsPressed = ni.JumpPressed });

                    _dashPort.Handle(new DashInputData { IsPressed = ni.DashHeld });
                    _slidePort.Handle(new SlideInputData { IsPressed = ni.SlideHeld });

                    if (ni.RunSpeedDelta != 0)
                    {
                        _runSpeedPort.Handle(new RunSpeedInputData { Delta = ni.RunSpeedDelta });
                    }

                    _wallRunInputPort.Handle(new WallRunInputData
                    {
                        MoveX = ni.Move.x,
                        MoveY = ni.Move.y,
                        IsClimbPressed = ni.ClimbHeld,
                        IsDescendPressed = ni.DescendHeld,
                    });
                }
                else if (Object.HasInputAuthority)
                {
                    // Client-side visual camera update only (no gameplay state mutation).
                    _lookPort.Handle(new LookInputData { X = ni.Look.x, Y = ni.Look.y });
                }
            }

            if (Object.HasStateAuthority)
            {
                _wallRunTickInputPort.Tick(Runner.DeltaTime);
            }
        }

        private void ResolvePorts()
        {
            if (_isResolved)
            {
                return;
            }

            LifetimeScope runtimeScope = LifetimeScope.Find<LifetimeScope>();
            if (runtimeScope == null || runtimeScope.Container == null)
            {
                return;
            }

            if (!TryResolveConfig(runtimeScope.Container, out CharacterConfigData characterConfigData, out CameraConfigData cameraConfigData))
            {
                return;
            }

            RigidBodyAdaptor rigidBodyAdaptor = GetComponent<RigidBodyAdaptor>();
            FpsCameraAdaptor fpsCameraAdaptor = GetComponentInChildren<FpsCameraAdaptor>(true);
            if (rigidBodyAdaptor == null || fpsCameraAdaptor == null)
            {
                Debug.LogWarning("PlayerInputTickBridge: Missing RigidBodyAdaptor or FpsCameraAdaptor on player prefab.");
                return;
            }

            IReadOnlyList<IMoveSpeedOutput> moveSpeedOutputs = Array.Empty<IMoveSpeedOutput>();
            if (runtimeScope.Container.TryResolve<IReadOnlyList<IMoveSpeedOutput>>(out var resolvedMoveSpeedOutputs))
            {
                moveSpeedOutputs = resolvedMoveSpeedOutputs;
            }

            CharacterEntity characterEntity = new CharacterEntity();
            Movement movement = new Movement(
                characterEntity,
                characterConfigData,
                rigidBodyAdaptor,
                rigidBodyAdaptor,
                rigidBodyAdaptor,
                rigidBodyAdaptor,
                moveSpeedOutputs);

            IWallRunCameraOutput[] wallRunCameraOutputs = { fpsCameraAdaptor };
            WallRun wallRun = new WallRun(characterEntity, characterConfigData, rigidBodyAdaptor, wallRunCameraOutputs);

            IControlRotationOutput[] controlRotationOutputs = { movement, wallRun };
            ICameraRotationOutput[] cameraRotationOutputs = { fpsCameraAdaptor };
            CameraLook cameraLook = new CameraLook(cameraRotationOutputs, cameraConfigData, controlRotationOutputs);

            Environment environment = new Environment(characterEntity);

            _jumpPort = movement;
            _movePort = movement;
            _dashPort = movement;
            _slidePort = movement;
            _runSpeedPort = movement;
            _lookPort = cameraLook;
            _wallRunInputPort = wallRun;
            _wallSenseInputPort = wallRun;
            _wallRunTickInputPort = wallRun;

            WallDetecter wallDetecter = GetComponent<WallDetecter>();
            if (wallDetecter != null)
            {
                wallDetecter.SetWallSenseInputPort(_wallSenseInputPort);
                if (movement is IWallSenseInputPort movementWallSense)
                {
                    wallDetecter.SetAdditionalWallSenseInputPort(movementWallSense);
                }
            }

            EnviromentAdaptor enviromentAdaptor = GetComponent<EnviromentAdaptor>();
            if (enviromentAdaptor != null)
            {
                enviromentAdaptor.SetEnvironmentInputPort(environment);
            }

            _isResolved = true;
        }

        private static bool TryResolveConfig(IObjectResolver container, out CharacterConfigData characterConfigData, out CameraConfigData cameraConfigData)
        {
            if (!container.TryResolve<CharacterConfigData>(out characterConfigData))
            {
                cameraConfigData = default;
                return false;
            }

            if (!container.TryResolve<CameraConfigData>(out cameraConfigData))
            {
                return false;
            }

            return true;
        }

        private void SetLocalInputEnabled(bool enabled)
        {
            InputBuffer buffer = GetComponent<InputBuffer>();
            if (buffer != null)
            {
                buffer.enabled = enabled;
            }

            PlayerInput playerInput = GetComponent<PlayerInput>();
            if (playerInput != null)
            {
                playerInput.enabled = enabled;
            }

            FpsCameraAdaptor fpsCameraAdaptor = GetComponentInChildren<FpsCameraAdaptor>(true);
            if (fpsCameraAdaptor != null)
            {
                fpsCameraAdaptor.SetUseMainCamera(enabled);
            }
        }
    }
}
