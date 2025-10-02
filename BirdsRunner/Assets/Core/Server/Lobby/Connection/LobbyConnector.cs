using System;
using Mirror;
using UnityEngine;
using UnityEngine.Events;

namespace Server.Lobby
{
    public class LobbyConnector : NetworkBehaviour
    {
        [NonSerialized] public static UnityEvent OnInitialized = new();
        [NonSerialized] public UnityEvent<LobbyData> OnDataSynchronize = new();

        //Server
        private LobbyConnectorsController _controller;
        private LobbyPlayerData _player_data;

        public LobbyPlayerData ServerPlayerData => _player_data;

        //Client
        public static LobbyConnector Local;
        private LobbyDataSynchronizer _sinchronizer;
        private int _player_id;

        public LobbyData LobbyData => _sinchronizer.Data;
        public LobbyPlayerData ClientPlayerData => LobbyData.GetPlayer(_player_id);

        public void Initialize(LobbyConnectorsController controller, 
            LobbyPlayerData player_data, LobbyDataSynchronizer synchronizer)
        {
            _controller = controller;
            _player_data = player_data;
            TargetInitilize(synchronizer.netIdentity, player_data.PlayerId);
        }

        [TargetRpc]
        private void TargetInitilize(NetworkIdentity controller, int player_id)
        {
            Local = this;
            _sinchronizer = controller.GetComponentInChildren<LobbyDataSynchronizer>();
            _player_id = player_id;

            _sinchronizer.OnDataSynchronize.AddListener((data) => OnDataSynchronize.Invoke(data));
            OnInitialized.Invoke();
        }

        [Command]
        public void CommandStartGame() => _controller.StartGame(_player_data);

        [Command]
        public void CommandUpdateTutorialState(bool tutorial) => _controller.UpdateTutorialState(_player_data, tutorial);

        [Command]
        public void CommandUpdateColor(Color color) => _controller.UpdateColor(_player_data, color);

        [Command]
        public void CommandUpdateReadinessState(bool readiness) => _controller.UpdateReadinessState(_player_data, readiness);
    }
}