using System;
using Mirror;
using UnityEngine;
using UnityEngine.Events;

namespace Game.PlayerSide.Character
{
    public class PlayerCharacterCreator : NetworkBehaviour
    {
        [NonSerialized] public UnityEvent<PlayerCharacterController> OnCharacterCreated = new();

        [field: SerializeField]
        public PlayerController Controller { get; private set; }
        [SerializeField] private PlayerCharacterController _character_prefab;

        private PlayerCharacterController _character;

        public void CreateCharacter(Point point)
        {
            DestroyCharacter();

            _character = NetworkUtils.NetworkInstantiate(_character_prefab, point.Position, point.Rotation);
            Controller.Player.AddNetworkObject(_character.netIdentity);
            _character.Initialize(Controller);
            OnCharacterCreated.Invoke(_character);
        }

        public void DestroyCharacter()
        {
            if (_character == null) return;
            Controller.Player.RemoveNetworkObjectFromList(_character.netIdentity);
            NetworkServer.Destroy(_character.gameObject);
            _character = null;
        }

        public PlayerCharacterController Character { get { return _character; } }
    }
}