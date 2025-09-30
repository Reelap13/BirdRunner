using System;
using Mirror;
using UnityEngine;
using UnityEngine.Events;
using Game.PlayerCamera;

namespace Game.PlayerSide.Character
{
    public class PlayerCharacterCreator : NetworkBehaviour
    {
        [NonSerialized] public UnityEvent<PlayerCharacterController> OnCharacterCreated = new();

        [field: SerializeField]
        public PlayerController Controller { get; private set; }
        [SerializeField] private PlayerCharacterController _character_prefab;
        [SerializeField] private GameObject cameraPointPrefab;
        private GameObject cameraPoint;

        private PlayerCharacterController _character;

        public void CreateCharacter(Point point)
        {
            DestroyCharacter();

            _character = NetworkUtils.NetworkInstantiate(_character_prefab, point.Position, point.Rotation);
            Controller.Player.AddNetworkObject(_character.netIdentity);
            _character.Initialize(Controller);
            if(cameraPoint == null)
            {
                cameraPoint = NetworkUtils.NetworkInstantiate(cameraPointPrefab, transform, null);
                SetCameraPoint(cameraPoint);
            }
            cameraPoint.GetComponent<CameraController>().SetTarget(_character.transform);
            cameraPoint.transform.position = _character.transform.position;
            OnCharacterCreated.Invoke(_character);
        }

        [TargetRpc]
        private void SetCameraPoint(GameObject cam)
        {
            Camera.main.transform.SetParent(cam.transform);
            Camera.main.transform.position = cam.transform.position;
            Camera.main.transform.rotation = cam.transform.rotation;
        }

        public void DestroyCharacter()
        {
            if (_character == null) return;
            Controller.Player.RemoveNetworkObjectFromList(_character.netIdentity);
            NetworkServer.Destroy(_character.gameObject);
            _character = null;
        }

        public PlayerCharacterController Character { get { return _character; } }
    }
}