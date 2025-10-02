using Mirror;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConnectorToServer : MonoBehaviour
{
    [SerializeField] private NetworkManager _manager;

    private void Awake()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void CreateHostConnection()
    {
        _manager.StartHost();
    }

    public void CreateClientConnection(string address)
    {
        _manager.networkAddress = address;
        _manager.StartClient();
    }
}
