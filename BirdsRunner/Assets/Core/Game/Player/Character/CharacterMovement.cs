using Unity.Mathematics;
using UnityEngine;
using Mirror;

namespace Game.PlayerSide.Character
{
    public class CharacterMovement : NetworkBehaviour
    {
        [field: SerializeField]
        public PlayerCharacterController Character { get; private set; }

        [SerializeField] private float _forward_speed = 1f;
        [SerializeField] private float _side_speed = 2f;
        [SerializeField] private Rigidbody _rb;
        [SerializeField] private Transform model;

        [SerializeField] private float maxBankAngle = 30f;
        [SerializeField] private float bankSmoothTime = 0.2f;
        [SerializeField] private float maxPitchAngle = 15f;
        [SerializeField] private float pitchSmoothTime = 0.2f;

        private float _currentBankAngle = 0f;
        private float _bankVelocity = 0f;
        private float _currentPitchAngle = 0f;
        private float _pitchVelocity = 0f;
        private Vector3 _localOffset;

        private Vector3 _side_direction = Vector3.zero;

        private MovingPlaneController plane;


        public void UpdateSideDirection(float2 side_direction)
        {
            _side_direction = transform.TransformDirection(new Vector3(side_direction.x, side_direction.y, 0));
        }

        public void Start()
        {
            plane = FindFirstObjectByType<MovingPlaneController>();
        }

        private void Update()
        {
            if (!isOwned) return;
            if (Character.State == CharacterState.DEAD)
                return;

            Move();
        }

        private void Move()
        {
            MoveForward();
            MoveSide();
        }

        private void MoveForward()
        {
            _localOffset = Quaternion.Inverse(plane.transform.rotation) * (transform.position - plane.transform.position);
            Vector3 birdNewPosition = plane.transform.position + plane.transform.rotation * _localOffset;
            Debug.Log(birdNewPosition);
            _rb.MovePosition(birdNewPosition);
            transform.rotation = plane.transform.rotation;
        }


        private void MoveSide()
        {
            float targetBankAngle = -_side_direction.x * maxBankAngle;
            _currentBankAngle = Mathf.SmoothDampAngle(_currentBankAngle, targetBankAngle, ref _bankVelocity, bankSmoothTime);

            float targetPitchAngle = -_side_direction.y * maxPitchAngle;
            _currentPitchAngle = Mathf.SmoothDampAngle(_currentPitchAngle, targetPitchAngle, ref _pitchVelocity, pitchSmoothTime);

            model.localRotation = Quaternion.Euler(_currentPitchAngle, 0, _currentBankAngle);
            Debug.Log(_rb.position);
            _rb.MovePosition(_rb.position + _side_direction * _side_speed * Time.deltaTime);

        }

    }
}