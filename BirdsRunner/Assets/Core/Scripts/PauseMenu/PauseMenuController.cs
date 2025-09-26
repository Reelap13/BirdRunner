using Mirror;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace UI.PauseMenu
{
    public class PauseMenuController : Singleton<PauseMenuController>
    {
        [SerializeField] private GameObject _pause_menu;
        [SerializeField] private GameObject _settings_menu;

        private bool _is_blocked = false;
        private bool _is_opened = false;
        private bool _is_settings_menu_opened = false;

        private void Start()
        {
            _pause_menu.SetActive(false);
            _settings_menu.SetActive(false);
            //InputManager.Instance.GetControls().UI.PauseMenu.started += ProcessInteraction;
        }

        private void ProcessInteraction(InputAction.CallbackContext _)
        {
            if (_is_blocked)
                return;

            if (!_is_opened)
                OpenMenu();
            else if (_is_settings_menu_opened)
                CloseSettingsMenu();
            else CloseMenu();
        }

        private void OpenMenu()
        {
            _is_opened = true;

            /*CharacterData.LocalCharacter.GetComponent<Character>().Disactivate();
            CharacterData.LocalCharacter.GetComponentInChildren<InterfaceController>().DiactivateGameUIPause();*/

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            _pause_menu.SetActive(true);
        }

        public void CloseMenu()
        {
            _is_opened = false;

            /*CharacterData.LocalCharacter.GetComponent<Character>().Activate();
            CharacterData.LocalCharacter.GetComponentInChildren<InterfaceController>().ActivateGameUIPause();*/
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            _pause_menu.SetActive(false);
        }

        public void OpenSettingsMenu()
        {
            _is_settings_menu_opened = true;
            _settings_menu.SetActive(true);
            _pause_menu.SetActive(false);
        }

        public void CloseSettingsMenu()
        {
            _is_settings_menu_opened = false;
            _settings_menu.SetActive(false);
            _pause_menu.SetActive(true);
        }

        public void ComebackToMenu()
        {
            var network_manager = NetworkManager.singleton;
            if (NetworkServer.active)
                network_manager.StopHost();
            else network_manager.StopClient();
        }

        public void BlockInteraction() => _is_blocked = true;
        public void UnblockInteraction() => _is_blocked = false;
    }
}