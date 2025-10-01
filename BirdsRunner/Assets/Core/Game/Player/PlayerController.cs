using System;
using Game.PlayerSide.Character;
using Mirror;
using Server.Data;
using UnityEngine;
using UnityEngine.Events;

namespace Game.PlayerSide
{
    public class PlayerController : NetworkBehaviour
    {
        [NonSerialized] public UnityEvent OnServerInitialized = new();
        [NonSerialized] public UnityEvent OnClientInitialized = new();

        [field: SerializeField]
        public PlayerCharacterCreator CharacterCreator { get; private set; }
        [field: SerializeField]
        public PlayerHealth Health { get; private set; }
        [field: SerializeField]
        public FeathersInventory Feathers { get; private set; }

        public Player Player { get; private set; }
        public Guid UniqueId => Player.ClientId;

        public void Initialize(Player player)
        {
            Player = player;
            OnServerInitialized.Invoke();
        }

        public override void OnStartAuthority()
        {
            OnClientInitialized.Invoke();
        }
    }
}