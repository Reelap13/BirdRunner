using Mirror;
using UnityEngine;

namespace Game.PlayerSide.Character
{
    public class PlayerCharacterController : NetworkBehaviour
    {
        [field: SerializeField]
        public DamageTaker DamageTaker { get; private set; }

        public PlayerController PlayerController { get; private set; }
        public static PlayerCharacterController Local;
        public CharacterState State { get; set; }


        public void Initialize(PlayerController player_controller)
        {
            PlayerController = player_controller;
            State = CharacterState.ALIVE;
        }

        public override void OnStartAuthority()
        {
            Local = this;
        }

        // Update to Target method if will be used NetworkTrasform(Client to server)
        public void UpdateCharacterPosition(Vector3 position)
        {
            transform.position = position;
        }
    }
}