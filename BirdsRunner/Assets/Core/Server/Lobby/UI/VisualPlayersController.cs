using NUnit.Framework;
using Server.Lobby.UI;
using UnityEngine;

namespace Server.Lobby
{
    public class VisualPlayersController : MonoBehaviour
    {
        [SerializeField] private VisualPlayer _first_player;
        [SerializeField] private VisualPlayer _second_player;

        public void UpdateUI(LobbyData data, LobbyPlayerData player_data)
        {
            if (data.Players.Count == 0)
                return;

            if (data.Players.Count >= 1)
                ProcessPlayer(data.Players[0], _first_player);  

            if (data.Players.Count == 2)
                ProcessPlayer(data.Players[1], _second_player);
            else ProcessPlayer(null, _second_player);
        }

        private void ProcessPlayer(LobbyPlayerData player, VisualPlayer character)
        {
            if (player == null)
            {
                character.UnsetPlayer();
                return;
            }

            character.SetPlayer(player);
        }
    }
}