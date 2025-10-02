using System;
using Game;
using Mirror;
using UnityEngine;
using UnityEngine.Events;

namespace Server.Lobby
{
    public class LobbyController : NetworkBehaviour
    {
        [NonSerialized] public UnityEvent OnGameStarted = new();

        [field: SerializeField]
        public PlayersConnectionController Players { get; private set; }
        [field: SerializeField]
        public LobbyConnectorsController Connectors { get; private set; }
        [field: SerializeField]
        public LobbyDataSynchronizer Synchronizer { get; private set; }

        [SerializeField] private GameInitializer _game_initializer_prefab;
        [SerializeField] private Vector2Int _players_number_to_start = new(1, 2);

        public LobbyData Data { get; private set; } = new();

        public override void OnStartServer()
        {
            Players.Initialize();
            Connectors.Initialize();
            Synchronizer.Initialize();
        }

        public void StartGame()
        {
            if (!IsReadyToStart())
                return;
            
            GameInitializer game_controller = CreateGameInitializer();
            game_controller.Initialize(Data);

            OnGameStarted.Invoke();
            Destroy(gameObject);
        }

        private GameInitializer CreateGameInitializer()
        {
            return NetworkUtils.NetworkInstantiate(_game_initializer_prefab);
        }

        public bool IsReadyToStart()
        {
            /*return Data.IsReady() &&
                _players_number_to_start.x <= Data.Players.Count &&
                Data.Players.Count <= _players_number_to_start.y;*/
            return _players_number_to_start.x <= Data.Players.Count &&
                Data.Players.Count <= _players_number_to_start.y;
        }
    }
}