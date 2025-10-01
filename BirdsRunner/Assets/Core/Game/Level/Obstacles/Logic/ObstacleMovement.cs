using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

namespace Game.Level.Obstacles
{
    public class ObstacleMovement : ObstacleBehaviour
    {
        [SerializeField] private Transform _body;
        [SerializeField] private float _speed = 2f;
        [SerializeField] private List<Transform> _points;
        [SerializeField] private bool _is_looped = false;
        [SerializeField] private bool _spawn_on_first_point = true;

        private float LIMIT = 0.01f;

        protected override void ActivateBehaviour()
        {
            if (_spawn_on_first_point)
                transform.position = _points[0].position;

            StartCoroutine(StartMoving());
        }

        private IEnumerator StartMoving()
        {
            int index = 0;
            if (_spawn_on_first_point)
                index = 1;

            while (true)
            {
                if (index >= _points.Count)
                {
                    if (_is_looped)
                        index = 0;
                    else break;
                }

                Transform point = _points[index];
                while (true)
                {
                    Vector3 direction = (point.position - _body.position).normalized;
                    float distance = Vector3.Distance(_body.position, point.position);
                    if (distance < LIMIT)
                        break;

                    Vector3 movement = direction * Mathf.Min(distance, _speed * Time.deltaTime);
                    _body.position += movement;
                    yield return null;
                }
                ++index;
            }
        }
    }
}