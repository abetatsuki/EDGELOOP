using System;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

namespace Runtime
{
    public class PlayerRuntimeFactory : IPlayerRuntimeFactory
    {
        private readonly CharacterConfigData _characterConfigData;
        private readonly CameraConfigData _cameraConfigData;
        private readonly IObjectResolver _resolver;

        public PlayerRuntimeFactory(
            CharacterConfigData characterConfigData,
            CameraConfigData cameraConfigData,
            IObjectResolver resolver)
        {
            _characterConfigData = characterConfigData;
            _cameraConfigData = cameraConfigData;
            _resolver = resolver;
        }

        public PlayerRuntimeBindings Create(GameObject playerRoot)
        {
            if (playerRoot == null)
            {
                return null;
            }

            RigidBodyAdaptor rigidBodyAdaptor = playerRoot.GetComponent<RigidBodyAdaptor>();
            FpsCameraAdaptor fpsCameraAdaptor = playerRoot.GetComponentInChildren<FpsCameraAdaptor>(true);
            if (rigidBodyAdaptor == null || fpsCameraAdaptor == null)
            {
                Debug.LogWarning("PlayerRuntimeFactory: Missing RigidBodyAdaptor or FpsCameraAdaptor on player root.");
                return null;
            }

            IReadOnlyList<IMoveSpeedOutput> moveSpeedOutputs = Array.Empty<IMoveSpeedOutput>();
            if (_resolver.TryResolve<IReadOnlyList<IMoveSpeedOutput>>(out var resolvedMoveSpeedOutputs))
            {
                moveSpeedOutputs = resolvedMoveSpeedOutputs;
            }

            CharacterEntity characterEntity = new CharacterEntity();
            Movement movement = new Movement(
                characterEntity,
                _characterConfigData,
                rigidBodyAdaptor,
                rigidBodyAdaptor,
                rigidBodyAdaptor,
                rigidBodyAdaptor,
                moveSpeedOutputs);

            IWallRunCameraOutput[] wallRunCameraOutputs = { fpsCameraAdaptor };
            WallRun wallRun = new WallRun(characterEntity, _characterConfigData, rigidBodyAdaptor, wallRunCameraOutputs);

            IControlRotationOutput[] controlRotationOutputs = { movement, wallRun };
            ICameraRotationOutput[] cameraRotationOutputs = { fpsCameraAdaptor };
            CameraLook cameraLook = new CameraLook(cameraRotationOutputs, _cameraConfigData, controlRotationOutputs);

            Environment environment = new Environment(characterEntity);

            return new PlayerRuntimeBindings(
                movement,
                movement,
                movement,
                movement,
                movement,
                cameraLook,
                wallRun,
                wallRun,
                wallRun,
                movement,
                environment);
        }
    }
}
