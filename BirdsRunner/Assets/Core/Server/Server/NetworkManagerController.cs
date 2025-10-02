using System;
using Mirror;
using UnityEngine;
using UnityEngine.Events;

namespace Server.ServerSide
{
    public class NetworkManagerController : NetworkManager
    {
        [NonSerialized] public UnityEvent OnClientGettedError = new();

        public override void OnServerDisconnect(NetworkConnectionToClient conn)
        {
            ConnectionController.Instance.UnregisterClient(conn);

            base.OnServerDisconnect(conn);
        }

        public override void OnClientDisconnect()
        {
            OnClientGettedError.Invoke();
        }

        public override void OnClientError(TransportError error, string reason)
        {
            OnClientGettedError.Invoke();
        }
    }
}