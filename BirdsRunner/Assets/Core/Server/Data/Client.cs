using System;
using UnityEngine;

namespace Server.Data
{
    public class Client
    {
        public Guid Id;
        public ConnectionType ConnectionType;
        public int ConnectionId;

        public Client(Guid id, ConnectionType connection_type, int connection_id)
        {
            Id = id;
            ConnectionType = connection_type;
            ConnectionId = connection_id;
        }
    }
}