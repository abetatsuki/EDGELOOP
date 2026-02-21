using System;
using Fusion;
using UnityEngine;
using UnityEngine.InputSystem;

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
        private IWallSenseInputPort _additionalWallSenseInputPort;
        private IEnvironmentInputPort _environmentInputPort;
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

            IPlayerRuntimeFactory runtimeFactory = RuntimeServiceRegistry.PlayerRuntimeFactory;
            if (runtimeFactory == null)
            {
                return;
            }

            PlayerRuntimeBindings bindings = runtimeFactory.Create(gameObject);
            if (bindings == null)
            {
                return;
            }

            _jumpPort = bindings.JumpPort;
            _movePort = bindings.MovePort;
            _dashPort = bindings.DashPort;
            _slidePort = bindings.SlidePort;
            _runSpeedPort = bindings.RunSpeedPort;
            _lookPort = bindings.LookPort;
            _wallRunInputPort = bindings.WallRunInputPort;
            _wallRunTickInputPort = bindings.WallRunTickInputPort;
            _wallSenseInputPort = bindings.WallSenseInputPort;
            _additionalWallSenseInputPort = bindings.AdditionalWallSenseInputPort;
            _environmentInputPort = bindings.EnvironmentInputPort;

            WallDetecter wallDetecter = GetComponent<WallDetecter>();
            if (wallDetecter != null)
            {
                wallDetecter.SetWallSenseInputPort(_wallSenseInputPort);
                if (_additionalWallSenseInputPort != null)
                {
                    wallDetecter.SetAdditionalWallSenseInputPort(_additionalWallSenseInputPort);
                }
            }

            EnviromentAdaptor enviromentAdaptor = GetComponent<EnviromentAdaptor>();
            if (enviromentAdaptor != null)
            {
                enviromentAdaptor.SetEnvironmentInputPort(_environmentInputPort);
            }

            _isResolved = true;
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
