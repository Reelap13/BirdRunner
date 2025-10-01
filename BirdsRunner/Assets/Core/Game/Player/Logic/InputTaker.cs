using Mirror;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.PlayerSide
{
    public class InputTaker : NetworkBehaviour
    {
        [field: SerializeField]
        public PlayerController Controller { get; private set; }

        private float2 _side_direction;

        public override void OnStartAuthority()
        {
            InputManager.Instance.GetControls().Player.Attack.started += _ => CommandShoot();
        }

        private void Update()
        {
            if (!isOwned) return;

            TakeMovementInput();
        }

        private void TakeMovementInput()
        {
            float h = InputManager.Instance.GetControls().Player.Move.ReadValue<Vector2>().x;
            float v = InputManager.Instance.GetControls().Player.Move.ReadValue<Vector2>().y;

            float2 new_side_direction = new(h, v);
            if (!(new_side_direction.x == _side_direction.x && new_side_direction.y == _side_direction.y))
            {
                _side_direction = new_side_direction;
                CommandUpdateSideDirection(new_side_direction);
            }
        }

        private void TakeShootingInput(InputAction.CallbackContext _)
        {
            CommandShoot();
        }

        [Command]
        public void CommandUpdateSideDirection(float2 direction)
        {
            Controller.CharacterCreator.Character.Movement.UpdateSideDirection(direction);
        }

        [Command]
        public void CommandShoot()
        {
            Controller.CharacterCreator.Character.Shoot.TryToFire();
        }
    }
}