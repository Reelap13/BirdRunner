using UnityEngine;
using Mirror;
using System;

namespace Game.PlayerCamera
{
    public class CameraController : NetworkBehaviour
    {
        public Guid playerId;
        [Tooltip("The target Transform to follow")]
        public Transform target;

        [Tooltip("How smoothly the camera catches up to the target.")]
        [SerializeField] private float followSpeed = 5f;

        [Tooltip("The desired distance from the target.")]
        [SerializeField] private float distance = 5f;

        [Tooltip("The height offset from the target.")]
        [SerializeField] private float height = 2f;

        [Tooltip("Higher values mean faster rotation.")]
        [SerializeField] private float rotationDamping = 3f;


        private Vector3 _desiredPosition;
        private Quaternion _desiredRotation;

        void FixedUpdate()
        {
            if (!isServer) return;
            if (target == null)
            {
                return;
            }

            // Calculate the desired position based on distance, height, and target
            _desiredPosition = target.position + Vector3.up * height - target.forward * distance;

            //Smooth following
            transform.position = Vector3.Lerp(transform.position, _desiredPosition, followSpeed * Time.fixedDeltaTime);

            // Calculate rotation based on target
            _desiredRotation = Quaternion.LookRotation(target.position - transform.position);

            // Smoothly apply the rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, _desiredRotation, rotationDamping * Time.fixedDeltaTime);
        }

        public void SetTarget(Transform t)
        {
            target = t;
        }
    }
}
