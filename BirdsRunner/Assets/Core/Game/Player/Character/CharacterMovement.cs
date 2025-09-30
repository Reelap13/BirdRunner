using Unity.Mathematics;
using UnityEngine;
using Mirror;
using Game.PlayerCamera;

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

        [SerializeField] private float rotationSpeed = 10f;
        [SerializeField] private float maxBankAngle = 30f;
        [SerializeField] private float bankSmoothTime = 0.2f;
        [SerializeField] private float maxPitchAngle = 15f;
        [SerializeField] private float pitchSmoothTime = 0.2f;
        [SerializeField] private float constraintSmoothingFactor = 0.1f;

        private float _currentBankAngle = 0f;
        private float _bankVelocity = 0f;
        private float _currentPitchAngle = 0f;
        private float _pitchVelocity = 0f;

        private Vector3 _side_direction = Vector3.zero;

        private MovingPlaneController plane;
        private Vector3 _targetPosition;
        private Quaternion _targetRotation;


        public void UpdateSideDirection(float2 sideDirection)
        {
            _side_direction = transform.TransformDirection(new Vector3(sideDirection.x, sideDirection.y, 0));
        }

        public void Start()
        {
            plane = FindFirstObjectByType<MovingPlaneController>();
            _targetPosition = transform.position;
            _targetRotation = transform.rotation;
        }

        private void Update()
        {
            if (!isServer) return;
            if (Character.State == CharacterState.DEAD)
            {
                Debug.Log("dead");
                return;
            }
            CalculateMovement();  // Calculate movement targets in Update
        }


        private void FixedUpdate()
        {
            if (!isServer) return;
            // Apply movement and rotation in FixedUpdate
            Move();
        }

        private void CalculateMovement()
        {
            CalculateRotation();
            CalculateConstrainedPosition();
            CalculateSideMovement();
        }

        private void CalculateRotation()
        {
            // Calculate the interpolation factor this frame
            float t = rotationSpeed * Time.deltaTime;

            // Slerp towards the target rotation
            _targetRotation = Quaternion.Slerp(transform.rotation, plane.transform.rotation, t);
        }


        private void CalculateConstrainedPosition()
        {
            // Apply Plane Constraint
            Vector3 worldPosition = _rb.position;
            Vector3 localPosition = plane.transform.InverseTransformPoint(worldPosition);
            localPosition.z = 0;
            _targetPosition = plane.transform.TransformPoint(localPosition);

        }

        private void CalculateSideMovement()
        {
            // Calculate smooth side movement
            float targetBankAngle = -_side_direction.x * maxBankAngle;
            _currentBankAngle = Mathf.SmoothDampAngle(_currentBankAngle, targetBankAngle, ref _bankVelocity, bankSmoothTime);

            float targetPitchAngle = -_side_direction.y * maxPitchAngle;
            _currentPitchAngle = Mathf.SmoothDampAngle(_currentPitchAngle, targetPitchAngle, ref _pitchVelocity, pitchSmoothTime);
        }

        private void Move()
        {
            // Apply smoothing and movement using MovePosition and MoveRotation
            float t = rotationSpeed * Time.fixedDeltaTime; // Use fixedDeltaTime here
            Quaternion newRotation = Quaternion.Slerp(_rb.rotation, _targetRotation, t);
            _rb.MoveRotation(newRotation);

            // Smoothly move towards the constrained position
            Vector3 newPosition = Vector3.Lerp(_rb.position, _targetPosition, constraintSmoothingFactor);

            _rb.MovePosition(newPosition + _side_direction * _side_speed * Time.fixedDeltaTime);
            model.localRotation = Quaternion.Euler(_currentPitchAngle, 0, _currentBankAngle);

        }

        //private void MoveForward()
        //{
        //    Vector3 worldPosition = _rb.position;

        //    Vector3 localPosition = plane.transform.InverseTransformPoint(worldPosition);

        //    localPosition.z = 0;

        //    Vector3 constrainedWorldPosition = plane.transform.TransformPoint(localPosition);

        //    // Calculate the interpolation factor this frame
        //    float t = rotationSpeed * Time.deltaTime;

        //    // Slerp towards the target rotation
        //    transform.rotation = Quaternion.Slerp(transform.rotation, plane.transform.rotation, t);

        //    _rb.MovePosition(constrainedWorldPosition);
        //}


        //private void MoveSide()
        //{
        //    float targetBankAngle = -_side_direction.x * maxBankAngle;
        //    _currentBankAngle = Mathf.SmoothDampAngle(_currentBankAngle, targetBankAngle, ref _bankVelocity, bankSmoothTime);

        //    float targetPitchAngle = -_side_direction.y * maxPitchAngle;
        //    _currentPitchAngle = Mathf.SmoothDampAngle(_currentPitchAngle, targetPitchAngle, ref _pitchVelocity, pitchSmoothTime);

        //    model.localRotation = Quaternion.Euler(_currentPitchAngle, 0, _currentBankAngle);
        //    _rb.MovePosition(_rb.position + _side_direction * _side_speed * Time.deltaTime);

        //}

    }
}