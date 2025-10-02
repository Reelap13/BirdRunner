using Server.Data;
using Server.ServerSide;
using UnityEngine;

namespace Server.Lobby
{
    [System.Serializable]
    public class LobbyPlayerData
    {
        public int PlayerId;
        public ConnectionType ConnectionType;
        public bool Readiness;
        public Color Color;

        public LobbyPlayerData() { }

        // only on server
        public Player GetPlayer() => PlayersController.Instance.GetPlayerById(PlayerId);
    }
}