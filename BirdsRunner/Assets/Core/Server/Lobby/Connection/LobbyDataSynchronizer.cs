using System;
using Mirror;
using UnityEngine;
using UnityEngine.Events;

namespace Server.Lobby
{
    public class LobbyDataSynchronizer : NetworkBehaviour
    {
        [NonSerialized] public UnityEvent<LobbyData> OnDataSynchronize = new();

        [SerializeField] private LobbyController _lobby;

        private LobbyData _data;

        public void Initialize()
        {
            _lobby.Players.OnPlayerAdded.AddListener(SynchronizeData);
            _lobby.Players.OnPlayerRemoved.AddListener(SynchronizeData);

            _lobby.Connectors.OnReadinessUpdated.AddListener(SynchronizeData);
            _lobby.Connectors.OnTutorialUpdated.AddListener(SynchronizeData);
        }

        private void SynchronizeData(LobbyPlayerData _) => SynchronizeData();
        private void SynchronizeData() => ClientSynchronizeData(_lobby.Data);

        [ClientRpc]
        private void ClientSynchronizeData(LobbyData data)
        {
            _data = data;
            OnDataSynchronize.Invoke(data);
        }

        public LobbyData Data { get { return _data; } }
    }
}