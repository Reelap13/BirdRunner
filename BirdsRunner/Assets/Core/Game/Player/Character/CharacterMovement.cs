using Unity.Mathematics;
using UnityEngine;

namespace Game.PlayerSide.Character
{
    public class CharacterMovement : MonoBehaviour
    {
        [field: SerializeField]
        public PlayerCharacterController Character { get; private set; }

        [SerializeField] private float _forward_speed = 1f;
        [SerializeField] private float _side_speed = 2f;
        [SerializeField] private Rigidbody _rb;

        private Vector3 _side_direction = Vector3.zero;

        public void UpdateSideDirection(float2 side_direction)
        {
            _side_direction = transform.TransformDirection(new Vector3(side_direction.x, side_direction.y, 0));
        }

        private void Update()
        {
            if (Character.State == CharacterState.DEAD)
                return;

            Move();
        }

        private void Move()
        {
            MoveForward();
            if (_side_direction != Vector3.zero)
                MoveSide();
        }

        private void MoveForward()
        {
            _rb.MovePosition(_rb.position + transform.forward * _forward_speed * Time.deltaTime);
        }

        private void MoveSide()
        {
            _rb.MovePosition(_rb.position + _side_direction * _side_speed * Time.deltaTime);
        }
    }
}