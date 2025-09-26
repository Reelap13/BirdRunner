using Mirror;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConnectorToServer : MonoBehaviour
{
    [SerializeField] private NetworkManager _manager;
    [SerializeField] private TMP_InputField _api_input_field;
    [SerializeField] private string _address = "localhost";

    private void Awake()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        _api_input_field.text = _address;
    }

    public void CreateHostConnection()
    {
        _manager.networkAddress = _address;
        _manager.StartHost();
    }

    public void CreateClientConnection()
    {
        _manager.networkAddress = _api_input_field.text;
        _manager.StartClient();
    }
}
