using System;
using System.Collections.Generic;
using Mirror;
using Server.Data;
using UnityEngine;
using UnityEngine.Events;

namespace Server.ServerSide
{
    public class ConnectionController : Singleton<ConnectionController>
    {
        [NonSerialized] public UnityEvent<Client> OnConnected = new();
        [NonSerialized] public UnityEvent<Client> OnDisconnected = new();

        private Dictionary<Guid, Client> _clients = new();

        public void RegisterClient(Guid guid, NetworkConnectionToClient conn)
        {
            ConnectionType connection_type = ConnectionType.CLIENT;
            if (_clients.Count == 0)
                connection_type = ConnectionType.HOST;

            Client client = new Client(guid, connection_type, conn.connectionId);
            _clients[guid] = client;
            OnConnected.Invoke(client);
        }

        public void UnregisterClient(NetworkConnectionToClient conn)
        {
            Client client = GetClientByConnection(conn);
            if (client == null)
            {
                Debug.LogError($"Try to disconnected a non-existent client with connection '{conn.connectionId}'");
                return;
            }

            _clients.Remove(client.Id);
            OnDisconnected.Invoke(client);
        }

        private Client GetClientByConnection(NetworkConnectionToClient conn) => GetClientByConnection(conn.connectionId);
        private Client GetClientByConnection(int connection_id)
        {
            foreach (var client in _clients.Values)
                if (client.ConnectionId == connection_id)
                    return client;
            return null;
        }

    }
}