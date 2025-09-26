using System;
using Mirror;
using Server.ServerSide;
using UnityEngine;

namespace Server.ClientSide
{
    public class ClientConnectionSender : NetworkBehaviour
    {
        [SerializeField] private bool _production_build = false;

        public override void OnStartLocalPlayer()
        {
            Guid player_id = GetPlayerId();
            CmdRegisterOnServer(player_id);
        }

        [Command]
        private void CmdRegisterOnServer(Guid player_id)
        {
            ConnectionController.Instance.RegisterClient(player_id, connectionToClient);
        }

        private Guid GetPlayerId()
        {
            return _production_build ? ClientIdentity.�lientId : ClientIdentity.GenerateId();
        }
    }
}