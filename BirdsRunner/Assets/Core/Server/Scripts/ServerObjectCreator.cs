using Mirror;
using NUnit.Framework;
using UnityEngine;

namespace Server.ServerSide
{
    public class ServerObjectCreator : NetworkBehaviour
    {
        [SerializeField] private GameObject _object_prefab;

        private GameObject _object;

        public override void OnStartServer()
        {
            CreateObject();
        }

        private void CreateObject()
        {
            _object = NetworkUtils.NetworkInstantiate(_object_prefab, transform);
            _object.transform.SetParent(transform, true);
        }

        public GameObject Object { get { return _object; } }    
    }
}