using System;
using System.Collections.Generic;
using Mirror;
using NUnit.Framework;
using Server.Data;
using UnityEngine;
using UnityEngine.Events;

namespace Server.ServerSide
{
    public class PlayersController : Singleton<PlayersController>
    {
        [NonSerialized] public UnityEvent<Player> OnConnected = new();
        [NonSerialized] public UnityEvent<Player> OnReconnected = new();
        [NonSerialized] public UnityEvent<Player> OnDisconnected = new();
        [NonSerialized] public UnityEvent<Player> OnDeleted = new();

        private List<Player> _players = new();
        private int _player_id = 0;

        private void Awake()
        {
            ConnectionController.Instance.OnConnected.AddListener(ConnectPlayer);
            ConnectionController.Instance.OnDisconnected.AddListener(DisconnectPlayer);
        }

        private void ConnectPlayer(Client client)
        {
            if (GetPlayer(client) != null)
            {
                ReconnectPlayer(client);
                return;
            }

            Player player = new Player(_player_id++, client);
            _players.Add(player);
            OnConnected.Invoke(player);
        }

        private void ReconnectPlayer(Client client)
        {
            Player player = GetPlayer(client);
            player.Client = client;
            EnableAllPlayerObjects(player);
            OnReconnected.Invoke(player);
        }

        private void DisconnectPlayer(Client client)
        {
            Player player = GetPlayer(client);
            if (player == null)
            {
                Debug.LogError($"Try to disconnected client without player object");
                return;
            }

            player.Client = null;
            DisableAllPlayerObjects(player); 
            OnDisconnected.Invoke(player);
        }

        public void DeletePlayer(Player player)
        {
            if (player.Client != null)
                return;

            _players.Remove(player);
            DisableAllPlayerObjects(player);
            OnDeleted.Invoke(player);
        }

        private Player GetPlayer(Client client)
        {
            foreach (Player player in _players) 
                if (player.ClientId == client.Id)
                    return player;
            return null;
        }


        private void DisableAllPlayerObjects(Player player)
        {
            foreach (var obj in player.OwnObjects)
                obj.RemoveClientAuthority();
        }

        private void EnableAllPlayerObjects(Player player)
        {
            NetworkConnectionToClient conn = player.GetConnection();
            foreach (var obj in player.OwnObjects)
            {
                obj.AssignClientAuthority(conn);
            }
        }

        public Player GetPlayerById(int id)
        {
            foreach (var player in _players)
                if (player.PlayerId == id)
                    return player;
            return null;
        }

        public List<Player> Players { get { return _players; } }
    }
}