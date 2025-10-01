using System.Collections;
using Mirror;
using UnityEngine;

namespace Game.Level.Obstacles
{
    public class ObstacleRotation : ObstacleBehaviour
    {
        [SerializeField] private Transform _body;
        [SerializeField] private Vector3 _rotate_direction;
        [SerializeField] private float _rotation_speed = 5f;

        protected override void ActivateBehaviour()
        {
            Debug.Log("ZZZ Start");
            StartCoroutine(StartRotation());
        }

        private IEnumerator StartRotation()
        {
            Debug.Log("ZZZ rotation");
            while (true)
            {
                _body.localRotation *= Quaternion.Euler(_rotate_direction * _rotation_speed * Time.deltaTime);
                yield return null;
            }
        }
    }
}