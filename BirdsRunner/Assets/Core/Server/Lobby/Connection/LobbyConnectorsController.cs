using System;
using System.Collections.Generic;
using Mirror;
using Server.Data;
using Server.ServerSide;
using UnityEngine;
using UnityEngine.Events;

namespace Server.Lobby
{
    public class LobbyConnectorsController : NetworkBehaviour
    {
        [NonSerialized] public UnityEvent OnReadinessUpdated = new();
        [NonSerialized] public UnityEvent OnTutorialUpdated = new();

        [SerializeField] private LobbyController _lobby;
        [SerializeField] private LobbyConnector _connector_prefab;

        private Dictionary<int, LobbyConnector> _connectors = new();

        public void Initialize()
        {
            _lobby.Players.OnPlayerAdded.AddListener(CreateConnector);
            _lobby.Players.OnPlayerRemoved.AddListener(DestroyConnector);

            _lobby.OnGameStarted.AddListener(DestroyConnectors);
        }

        public void UpdateReadinessState(LobbyPlayerData data, bool readiness)
        {
            data.Readiness = readiness;
            OnReadinessUpdated.Invoke();
        }

        public void UpdateTutorialState(LobbyPlayerData data, bool tutorial)
        {
            if (data.ConnectionType != ConnectionType.HOST)
                return;

            _lobby.Data.Tutorial = tutorial;
            OnTutorialUpdated.Invoke();
        }

        public void StartGame(LobbyPlayerData data)
        {
            if (data.ConnectionType != ConnectionType.HOST)
                return;

            _lobby.StartGame();
        }

        private void CreateConnector(LobbyPlayerData data)
        {
            LobbyConnector connector = NetworkUtils.NetworkInstantiate(_connector_prefab, transform);
            data.GetPlayer().AddNetworkObject(connector.netIdentity);
            _connectors.Add(data.PlayerId, connector);

            connector.Initialize(this, data, _lobby.Synchronizer);
        }

        private void DestroyConnector(LobbyPlayerData data)
        {
            Destroy(_connectors[data.PlayerId].gameObject);
            _connectors.Remove(data.PlayerId);
        }

        private void DestroyConnectors()
        {
            foreach (var connector in _connectors.Values)
            {
                connector.ServerPlayerData.GetPlayer().RemoveNetworkObjectFromList(connector.netIdentity);
                Destroy(connector.gameObject);
            }
            _connectors.Clear();
        }
    }
}