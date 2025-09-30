using Mirror;
using UnityEngine;

namespace Game.Level.Obstacles
{
    public class ObstacleRotation : NetworkBehaviour
    {
        [SerializeField] private ObstacleController _obstacle;
        [SerializeField] private Transform _body;
        [SerializeField] private Vector3 _rotate_direction;
        [SerializeField] private float _rotation_speed = 5f;

        private void Update()
        {
            if (!isServer)
                return;

            _body.localRotation *= Quaternion.Euler(_rotate_direction * _rotation_speed * Time.deltaTime);
        }
    }
}