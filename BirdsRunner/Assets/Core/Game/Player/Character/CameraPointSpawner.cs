using UnityEngine;
using Mirror;
using Game.PlayerCamera;

public class CameraPointSpawner : NetworkBehaviour
{
    [SerializeField] private GameObject cameraPointPrefab;
    [SyncVar] private GameObject cameraPoint;
    public override void OnStartServer()
    {
        base.OnStartServer();
        cameraPoint = NetworkUtils.NetworkInstantiate(cameraPointPrefab, transform, null);
        cameraPoint.GetComponent<CameraController>().SetTarget(transform);
    }

    public override void OnStartAuthority()
    {
        base.OnStartAuthority();
        Camera.main.transform.SetParent(cameraPoint.transform);
        Camera.main.transform.position = cameraPoint.transform.position;
        Camera.main.transform.rotation = cameraPoint.transform.rotation;

    }

    public void OnDestroy()
    {
        if (isOwned)
        {
            Camera.main.transform.SetParent(null);
        }
        Destroy(cameraPoint);
        NetworkServer.Destroy(cameraPoint);
        cameraPoint = null;
    }
}
