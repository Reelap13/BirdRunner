using UnityEngine;

namespace Game.PlayerCamera
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private float distance = 10f;
        [SerializeField] private float height = 5f;
        [SerializeField] private float positionDamping = 2f;
        [SerializeField] private float rotationDamping = 1f;
        [SerializeField] private Vector3 lookOffset = Vector3.zero;


        public void MoveCamera()
        {
            if (!target)
            {
                Debug.LogWarning("CameraFollow: No target assigned!");
                return;
            }

            Vector3 desiredPosition = target.position - target.forward * distance + Vector3.up * height;

            desiredPosition += target.rotation * lookOffset;

            transform.position = Vector3.Lerp(transform.position, desiredPosition, positionDamping * Time.deltaTime);

            Quaternion desiredRotation = Quaternion.LookRotation(target.position + (target.rotation * lookOffset) - transform.position);

            transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, rotationDamping * Time.deltaTime);
        }

        public void SetTarget(Transform t)
        {
            target = t;
        }
    }
}
