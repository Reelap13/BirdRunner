using Mirror;
using UnityEngine;
using Game.PlayerCamera;

namespace Game.PlayerSide.Character
{
    public class PlayerCharacterController : NetworkBehaviour
    {
        [field: SerializeField]
        public CharacterMovement Movement { get; private set; }
        [field: SerializeField]
        public PlayerShoot Shoot { get; private set; }
        [field: SerializeField]
        public DamageTaker DamageTaker { get; private set; }

        public PlayerController PlayerController { get; private set; }
        public static PlayerCharacterController Local;
        public CharacterState State { get; set; } = CharacterState.DEAD;


        public void Initialize(PlayerController player_controller)
        {
            PlayerController = player_controller;
            State = CharacterState.ALIVE;
        }

        public override void OnStartAuthority()
        {
            Local = this;
            State = CharacterState.ALIVE;
        }

        // Update to Target method if will be used NetworkTrasform(Client to server)
        public void UpdateCharacterPosition(Vector3 position)
        {
            transform.position = position;
        }
    }
}