using System;
using System.Collections.Generic;
using Fusion;
using Fusion.Sockets;
using UnityEngine;

namespace Runtime
{
    public class FusionInputFromBuffer : MonoBehaviour, INetworkRunnerCallbacks
    {
        [Header("Input")]
        [SerializeField] private InputBuffer _buffer;

        [Header("Player Spawn")]
        [SerializeField] private NetworkPrefabRef _playerPrefab;
        [SerializeField] private Transform[] _spawnPoints;

        private readonly Dictionary<PlayerRef, NetworkObject> _spawnedPlayers = new Dictionary<PlayerRef, NetworkObject>();
        private readonly Dictionary<PlayerRef, InputBuffer> _buffersByPlayer = new Dictionary<PlayerRef, InputBuffer>();
        private NetworkRunner _runner;

        public void SetBuffer(PlayerRef player, InputBuffer buffer)
        {
            if (buffer == null)
            {
                _buffersByPlayer.Remove(player);
                if (_runner != null && player == _runner.LocalPlayer)
                {
                    _buffer = null;
                }
                return;
            }

            _buffersByPlayer[player] = buffer;
            if (_runner != null && player == _runner.LocalPlayer)
            {
                _buffer = buffer;
            }
        }

        private void Awake()
        {
            _runner = GetComponent<NetworkRunner>();
        }

        private void OnEnable()
        {
            if (_runner != null)
            {
                _runner.AddCallbacks(this);
                _runner.ProvideInput = true;
            }
        }

        private void OnDisable()
        {
            if (_runner != null)
            {
                _runner.RemoveCallbacks(this);
            }
        }

        public void OnInput(NetworkRunner runner, NetworkInput input)
        {
            _buffer = ResolveLocalBuffer(runner);

            if (_buffersByPlayer.TryGetValue(runner.LocalPlayer, out InputBuffer localBuffer))
            {
                _buffer = localBuffer;
            }

            if (_buffer == null)
            {
                input.Set(default(NetInput));
                return;
            }

            runner.ProvideInput = true;
            input.Set(_buffer.BuildNetInputAndConsumeOneShots());
        }

        public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
        {
            if (_playerPrefab.IsValid == false)
            {
                Debug.LogWarning("FusionInputFromBuffer: Player Prefab is not assigned.");
                return;
            }

            TrySpawnPlayer(runner, player);
        }

        public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
        {
            if (_spawnedPlayers.TryGetValue(player, out NetworkObject playerObject))
            {
                _spawnedPlayers.Remove(player);
                if (playerObject != null)
                {
                    runner.Despawn(playerObject);
                }
            }
        }

        private Vector3 ResolveSpawnPosition(PlayerRef player)
        {
            if (_spawnPoints == null || _spawnPoints.Length == 0)
            {
                return Vector3.zero;
            }

            int index = Math.Abs(player.RawEncoded % _spawnPoints.Length);
            Transform spawnPoint = _spawnPoints[index];
            return spawnPoint != null ? spawnPoint.position : Vector3.zero;
        }

        public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input) { }
        public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason) { }
        public void OnConnectedToServer(NetworkRunner runner)
        {
            // Shared mode can miss local OnPlayerJoined timing depending on startup order.
            if (runner.GameMode == GameMode.Shared && runner.LocalPlayer.IsRealPlayer)
            {
                TrySpawnPlayer(runner, runner.LocalPlayer);
            }
        }
        public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason) { }
        public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token) { }
        public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason) { }
        public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message) { }
        public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList) { }
        public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data) { }
        public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken) { }
        public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress) { }
        public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key, ArraySegment<byte> data) { }
        public void OnSceneLoadDone(NetworkRunner runner)
        {
            if (runner.GameMode == GameMode.Shared && runner.LocalPlayer.IsRealPlayer)
            {
                TrySpawnPlayer(runner, runner.LocalPlayer);
            }
        }
        public void OnSceneLoadStart(NetworkRunner runner) { }
        public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player) { }
        public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player) { }

        private void TrySpawnPlayer(NetworkRunner runner, PlayerRef player)
        {
            bool shouldSpawn;
            if (runner.GameMode == GameMode.Shared)
            {
                shouldSpawn = player == runner.LocalPlayer;
            }
            else
            {
                shouldSpawn = runner.IsServer;
            }

            if (!shouldSpawn)
            {
                return;
            }

            if (_spawnedPlayers.ContainsKey(player))
            {
                return;
            }

            NetworkObject existing = runner.GetPlayerObject(player);
            if (existing != null)
            {
                _spawnedPlayers[player] = existing;
                return;
            }

            Vector3 spawnPosition = ResolveSpawnPosition(player);
            NetworkObject playerObject = runner.Spawn(_playerPrefab, spawnPosition, Quaternion.identity, player);
            if (playerObject == null)
            {
                Debug.LogWarning($"FusionInputFromBuffer: Failed to spawn player for {player}.");
                return;
            }

            _spawnedPlayers[player] = playerObject;
            runner.SetPlayerObject(player, playerObject);
        }

        private static InputBuffer ResolveLocalBuffer(NetworkRunner runner)
        {
            if (!runner.LocalPlayer.IsRealPlayer)
            {
                return null;
            }

            NetworkObject localPlayerObject = runner.GetPlayerObject(runner.LocalPlayer);
            if (localPlayerObject == null)
            {
                return null;
            }

            InputBuffer buffer = localPlayerObject.GetComponent<InputBuffer>();
            if (buffer != null)
            {
                return buffer;
            }

            return localPlayerObject.GetComponentInChildren<InputBuffer>(true);
        }
    }
}
