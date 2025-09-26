using UnityEngine;
using UnityEngine.UI;

namespace Server.Lobby.UI
{
    public class LobbyUIController : MonoBehaviour
    {
        [SerializeField] private MainUI _main_ui;
        [SerializeField] private PlayerCardsController _player_cards;

        private void Start()
        {
            LobbyConnector.OnInitialized.AddListener(Initialize);
        }

        private void Initialize()
        {
            LobbyConnector.Local.OnDataSynchronize.AddListener(UpdateData);
        }

        private void UpdateData(LobbyData data)
        {
            _main_ui.UpdateUI(data, LobbyConnector.Local.ClientPlayerData);
            _player_cards.UpdateUI(data, LobbyConnector.Local.ClientPlayerData);
        }

        public void UpdateReadiness(bool rediness) => LobbyConnector.Local.CommandUpdateReadinessState(rediness);
        public void UpdateTutorial(bool tutorial) => LobbyConnector.Local.CommandUpdateTutorialState(tutorial);
        public void StartGame() => LobbyConnector.Local.CommandStartGame();
    }
}