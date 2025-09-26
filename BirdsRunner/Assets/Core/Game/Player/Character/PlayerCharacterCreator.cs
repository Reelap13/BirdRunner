using Mirror;
using UnityEngine;

namespace Game.PlayerSide.Character
{
    public class PlayerCharacterCreator : NetworkBehaviour
    {
        [field: SerializeField]
        public PlayerController Controller { get; private set; }
        [SerializeField] private PlayerCharacterController _character_prefab;

        private PlayerCharacterController _character;

        public override void OnStartServer()
        {
            GameController.Instance.OnNewGameStarted.AddListener(SetupOnGameStarting);    
            SavesController.Instance.OnLoadingStarted.AddListener(Load);    
            GameController.Instance.OnNewGameStarted.AddListener(Save);    
        }

        private void SetupOnGameStarting()
        {
            CreatePlayerCharacter();
        }

        private void Load()
        {

        }

        private void Save()
        {

        }

        private void CreatePlayerCharacter()
        {
            if (_character != null)
                DestroyPlayerCharacter();

            _character = NetworkUtils.NetworkInstantiate(_character_prefab);
            Controller.Player.AddNetworkObject(_character.netIdentity);
            _character.Initialize(Controller);
        }

        private void DestroyPlayerCharacter()
        {
            Controller.Player.RemoveNetworkObjectFromList(_character.netIdentity);
            NetworkServer.Destroy(_character.gameObject);
            _character = null;
        }

        public PlayerCharacterController Character { get { return _character; } }
    }
}