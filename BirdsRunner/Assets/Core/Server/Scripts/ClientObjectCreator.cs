using Mirror;
using UnityEngine;

namespace Server.ClientSide
{
    public class ClientObjectCreator : NetworkBehaviour
    {
        [SerializeField] private GameObject _prefab;
        [SerializeField] private Transform _spawn_point;

        public GameObject Object { get; private set; }

        public override void OnStartClient()
        {
            Transform spawn_point = _spawn_point;
            if (spawn_point == null)
                spawn_point = transform;

            Object = Instantiate(_prefab, spawn_point.position, spawn_point.rotation, spawn_point);
        }
    }
}