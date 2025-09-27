using UnityEngine;
using Mirror;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public float distance = 10f;
    public float height = 5f;
    public float positionDamping = 2f;
    public float rotationDamping = 1f;
    public Vector3 lookOffset = Vector3.zero;

    private void LateUpdate()
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
}
