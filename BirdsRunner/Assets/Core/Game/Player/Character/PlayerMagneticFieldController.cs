using UnityEngine;
using Mirror;

public class PlayerMagneticFieldController : NetworkBehaviour
{
    [SerializeField] private GameObject magneticFieldCollider;

    public void ActivateField()
    {
        magneticFieldCollider.SetActive(true);
        RpcActivateField();
    }

    public void DiactivateField()
    {
        magneticFieldCollider.SetActive(false);
        RpcDiactivateField();
    }

    [ClientRpc]
    private void RpcActivateField()
    {
        magneticFieldCollider.SetActive(true);
    }

    [ClientRpc]
    private void RpcDiactivateField()
    {
        magneticFieldCollider.SetActive(false);
    }
}
