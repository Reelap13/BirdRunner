using Mirror;
using Unity.Mathematics;
using UnityEngine;

namespace Game.PlayerSide
{
    public class InputTaker : NetworkBehaviour
    {
        [field: SerializeField]
        public PlayerController Controller { get; private set; }

        private float2 _side_direction;

        private void Update()
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

        [Command]
        public void CommandUpdateSideDirection(float2 direction)
        {
            Controller.CharacterCreator.Character.Movement.UpdateSideDirection(direction);
        }
    }
}