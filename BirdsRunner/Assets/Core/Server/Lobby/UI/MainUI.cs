using Mirror;
using Scripts.UI.Scene;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Server.Lobby.UI
{
    public class MainUI : MonoBehaviour
    {
        [SerializeField] private LobbyUIController _ui_controller;
        [SerializeField] private Transform _camera_point;

        [SerializeField] private Button _start_game_button;
        [SerializeField] private GameObject _not_enough_players;
        [SerializeField] private Button _exit_to_menu;

        [SerializeField] private bool _block_solo_starting = true;

        private LobbyPlayerData _player_data;

        private void Awake()
        {
            _start_game_button.onClick.AddListener(StartGame);
            _exit_to_menu.onClick.AddListener(ExitToMenu);
            StartCoroutine(SceneUI.Instance.Fader.FadeIn());

            Camera.main.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
        }

        public void UpdateUI(LobbyData data, LobbyPlayerData player)
        {
            _player_data = player;

            _not_enough_players.SetActive(false);
            if (player.ConnectionType != Data.ConnectionType.HOST)
            {
                _start_game_button.gameObject.SetActive(false);
                _not_enough_players.gameObject.SetActive(false);
                return;
            }
            _start_game_button.gameObject.SetActive(true);

            if (data.Players.Count != 2 && _block_solo_starting)
            {
                _start_game_button.interactable = false;
                _not_enough_players.SetActive(true);

                var colors = _start_game_button.colors;
                colors.normalColor = new Color(colors.normalColor.r, colors.normalColor.g, colors.normalColor.b, 0.5f);
                colors.disabledColor = new Color(colors.disabledColor.r, colors.disabledColor.g, colors.disabledColor.b, 0.3f);
                _start_game_button.colors = colors;
            }
            else
            {
                _start_game_button.interactable = true;

                var colors = _start_game_button.colors;
                colors.normalColor = new Color(colors.normalColor.r, colors.normalColor.g, colors.normalColor.b, 1f);
                _start_game_button.colors = colors;
            }
        }

        private void ChangeColor() => _ui_controller.UpdateColor(Random.ColorHSV());

        private void ExitToMenu()
        {
            var network_manager = NetworkManager.singleton;
            if (NetworkServer.active)
                network_manager.StopHost();
            else network_manager.StopClient();
        }

        private void StartGame() => _ui_controller.StartGame();
    }
}