using System;
using Mirror;
using Server.ServerSide;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ConnectorToServer : MonoBehaviour
{
    [NonSerialized] public UnityEvent OnError = new();

    [SerializeField] private NetworkManagerController _manager;

    private void Awake()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        _manager.OnClientGettedError.AddListener(() => OnError.Invoke());
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
