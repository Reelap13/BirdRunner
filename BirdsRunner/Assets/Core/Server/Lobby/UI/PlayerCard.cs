using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


namespace Server.Lobby.UI
{
    public class PlayerCard : MonoBehaviour
    {
        [NonSerialized] public UnityEvent<bool> OnReadinessUpdated = new();

        [SerializeField] private Toggle _readiness_toggle;
        [SerializeField] private GameObject _active_background;
        [SerializeField] private TextMeshProUGUI _name;

        public void UpdateUI(LobbyPlayerData player_data, bool is_own)
        {
            _readiness_toggle.onValueChanged.RemoveListener(UpdateReadiness);

            _readiness_toggle.isOn = player_data.Readiness;
            _readiness_toggle.enabled = is_own;
            _active_background.SetActive(is_own);
            if (is_own)
            {
                _readiness_toggle.onValueChanged.AddListener(UpdateReadiness);
            }

            if (player_data.PlayerId == 0)
                _name.text = "First Player";
            else _name.text = "Second Player";
        }

        private void UpdateReadiness(bool readiness) => OnReadinessUpdated.Invoke(readiness);
    }
}