using Scripts.UI.Scene;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Server.Lobby.UI
{
    public class MainUI : MonoBehaviour
    {
        [SerializeField] private LobbyUIController _ui_controller;

        [SerializeField] private Button _start_game_button;
        [SerializeField] private Toggle _tutorial_toggle;

        private void Awake()
        {
            _start_game_button.onClick.AddListener(StartGame);
            _tutorial_toggle.onValueChanged.AddListener(UpdateTutorialState);
            StartCoroutine(SceneUI.Instance.Fader.FadeIn());
        }

        public void UpdateUI(LobbyData data, LobbyPlayerData player)
        {
            if (player.ConnectionType != Data.ConnectionType.HOST)
            {
                _start_game_button.gameObject.SetActive(false);
                _tutorial_toggle.gameObject.SetActive(false);
                return;
            }

            _start_game_button.gameObject.SetActive(true);
            _tutorial_toggle.gameObject.SetActive(true);
            _tutorial_toggle.isOn = data.Tutorial;
        }

        private void StartGame() => _ui_controller.StartGame();
        private void UpdateTutorialState(bool tutorial) => _ui_controller.UpdateTutorial(tutorial);
    }
}