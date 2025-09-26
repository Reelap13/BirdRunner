using System;
using System.Collections.Generic;
using Mirror;
using NUnit.Framework;
using UnityEngine;

namespace Server.Data
{
    public class Player
    {
        public int PlayerId;
        public Guid ClientId;
        public ConnectionType ConnectionType;
        public Client Client;
        public List<NetworkIdentity> OwnObjects;

        public Player(int player_id, Client client)
        {
            PlayerId = player_id;
            ClientId = client.Id;
            ConnectionType = client.ConnectionType;
            Client = client;
            OwnObjects = new List<NetworkIdentity>();
        }

        public void AddNetworkObject(NetworkIdentity obj)
        {
            if (!IsHasClient())
                return;

            OwnObjects.Add(obj);
            obj.AssignClientAuthority(GetConnection());
        }

        public void RemoveNetworkObject(NetworkIdentity obj)
        {
            OwnObjects.Remove(obj);
            obj.RemoveClientAuthority();
        }

        public void RemoveNetworkObjectFromList(NetworkIdentity obj)
        {
            OwnObjects.Remove(obj);
        }

        public bool IsHasClient() => Client != null;
        public NetworkConnectionToClient GetConnection()
        {
            if (NetworkServer.connections.TryGetValue(Client.ConnectionId, out var conn))
                return conn;
            Debug.LogError("Try to get connection of player without conneciton");
            return null;
        }
    }
}