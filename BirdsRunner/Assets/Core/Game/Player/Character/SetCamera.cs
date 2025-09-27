using UnityEngine;
using Mirror;
using Unity.Cinemachine;

public class SetCamera : NetworkBehaviour
{
    public override void OnStartAuthority()
    {
        base.OnStartAuthority();
        CameraTarget target = new();
        target.TrackingTarget = transform;
        target.LookAtTarget = transform;
        FindFirstObjectByType<CinemachineCamera>().Target = target;
    }
}
