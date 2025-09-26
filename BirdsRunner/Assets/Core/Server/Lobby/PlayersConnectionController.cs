using System;
using System.Collections.Generic;
using System.Linq;
using Mirror;
using Server.Data;
using Server.ServerSide;
using UnityEngine;
using UnityEngine.Events;

namespace Server.Lobby
{
    public class PlayersConnectionController : NetworkBehaviour
    {
        [NonSerialized] public UnityEvent<LobbyPlayerData> OnPlayerAdded = new();
        [NonSerialized] public UnityEvent<LobbyPlayerData> OnPlayerRemoved = new();

        [SerializeField] private LobbyController _lobby;

        public List<LobbyPlayerData> Players => _lobby.Data.Players;

        public void Initialize()
        {
            PlayersController.Instance.OnConnected.AddListener(AddPlayer);
            PlayersController.Instance.OnDisconnected.AddListener(DisconnectPlayer);
        }

        private void AddPlayer(Player player)
        {
            LobbyPlayerData data = new LobbyPlayerData()
            {
                PlayerId = player.PlayerId,
                ConnectionType = player.ConnectionType,
            };

            Players.Add(data);
            OnPlayerAdded.Invoke(data);
        }

        private void DisconnectPlayer(Player player)
        {
            LobbyPlayerData removed_data = _lobby.Data.GetPlayer(player.PlayerId);
            Players.Remove(removed_data);
            PlayersController.Instance.DeletePlayer(player);
            OnPlayerRemoved.Invoke(removed_data);
        }
    }
}