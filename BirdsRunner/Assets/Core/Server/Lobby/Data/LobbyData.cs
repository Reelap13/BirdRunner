using System.Collections.Generic;
using Server.Data;
using Server.ServerSide;
using UnityEngine;

namespace Server.Lobby
{
    [System.Serializable]
    public class LobbyData
    {
        public List<LobbyPlayerData> Players = new();
        public bool Tutorial = false;

        public LobbyData() { }

        public LobbyPlayerData GetPlayer(int id)
        {
            foreach (var player in Players)
                if (player.PlayerId == id) return player;
            return null;
        }

        public bool IsReady()
        {
            foreach (var player in Players)
                if (!player.Readiness)
                    return false;
            return true;
        }
    }
}