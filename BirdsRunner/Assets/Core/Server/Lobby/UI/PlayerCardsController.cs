using System.Collections.Generic;
using UnityEngine;

namespace Server.Lobby.UI
{
    public class PlayerCardsController : MonoBehaviour
    {
        [SerializeField] private LobbyUIController _ui_controller;

        [SerializeField] private PlayerCard _card_prefab;

        private Dictionary<int, PlayerCard> _player_cards = new();

        public void UpdateUI(LobbyData data, LobbyPlayerData player_data)
        {
            if (data.Players.Count != _player_cards.Count)
                RecreateCards(data);

            foreach (var player in data.Players)
            {
                PlayerCard card = _player_cards[player.PlayerId];
                card.UpdateUI(player, player.PlayerId == player_data.PlayerId);
            }
        }

        private void RecreateCards(LobbyData data)
        {
            DestroyCards();
            _player_cards = new();
            foreach (var player in data.Players)
            {
                PlayerCard card = CreateCard();
                _player_cards.Add(player.PlayerId, card);
                card.OnReadinessUpdated.AddListener(UpdateReadiness);
            }
        }

        private PlayerCard CreateCard()
        {
            PlayerCard card = Instantiate(_card_prefab);
            card.transform.SetParent(transform, false);
            return card;
        }

        private void UpdateReadiness(bool readiness) => _ui_controller.UpdateReadiness(readiness);

        private void DestroyCards()
        {
            foreach (var card in _player_cards.Values)
                Destroy(card.gameObject);
            _player_cards.Clear();
        }
    }
}