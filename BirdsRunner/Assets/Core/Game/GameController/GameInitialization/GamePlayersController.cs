using System.Collections.Generic;
using System.Linq;
using Game.PlayerSide;
using Server.Lobby;
using UnityEngine;

namespace Game
{
    public class GamePlayersController : Singleton<GamePlayersController>
    {
        [SerializeField] private PlayerController _player_prefab;

        private Dictionary<int, PlayerController> _players = new();

        public void CreatePlayersControllers(LobbyData data)
        {
            foreach (var player_data in data.Players)
            {
                PlayerController player = NetworkUtils.NetworkInstantiate(_player_prefab);
                player_data.GetPlayer().AddNetworkObject(player.netIdentity);
                player.Initialize(player_data.GetPlayer());
                _players.Add(player_data.PlayerId, player);
            }
        }

        public List<PlayerController> Players => _players.Values.ToList();
    }
}