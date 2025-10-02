using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Server.Lobby
{
    public class VisualPlayer : MonoBehaviour
    {
        [SerializeField] private List<Transform> _way_points;
        [SerializeField] private float _movement_duration = 3f;
        [SerializeField] private float _hover_amplitude = 0.2f;
        [SerializeField] private float _hover_frequency = 2f;
        [SerializeField] private float _pitch_amplitude = 1f;

        private LobbyPlayerData _player;
        private int _way_point_id = 0;
        private bool _is_moving = false;

        private void Awake()
        {
            _way_point_id = 1;
            if (_way_points.Count > 0)
            {
                transform.position = _way_points[0].position;
                transform.rotation = _way_points[0].rotation;
            }
        }

        public void SetPlayer(LobbyPlayerData player)
        {
            _player = player;
            gameObject.SetActive(true);
            if (!_is_moving)
                StartCoroutine(Move());
        }

        public void UnsetPlayer()
        {
            _player = null;
            gameObject.SetActive(false);
            StopAllCoroutines();
            _is_moving = false;
        }

        private IEnumerator Move()
        {
            _is_moving = true;

            while (true)
            {
                Transform current = _way_points[_way_point_id % _way_points.Count];
                Vector3 start_position = transform.position;
                Vector3 end_position = current.position;

                float duration = _movement_duration * Random.Range(0.8f, 1.2f); 
                float _t = 0f;

                while (_t < 1f)
                {
                    _t += Time.deltaTime / duration;
                    float t_eased = Mathf.SmoothStep(0f, 1f, _t);

                    Vector3 base_position = Vector3.Lerp(start_position, end_position, t_eased);
                    
                    Vector3 hover_offset = new Vector3(
                        Mathf.Sin(Time.time * _hover_frequency) * _hover_amplitude,
                        Mathf.Sin(Time.time * _hover_frequency * 1.5f) * _hover_amplitude * 0.5f,
                        Mathf.Sin(Time.time * _hover_frequency * 0.8f) * _hover_amplitude * 0.2f
                    );

                    transform.position = base_position + hover_offset;

                    float pitch = Mathf.Sin(Time.time * _hover_frequency * 1.2f) * _pitch_amplitude;
                    transform.localRotation = Quaternion.Euler(pitch, transform.localEulerAngles.y, transform.localEulerAngles.z);

                    yield return null;
                }

                _way_point_id = (_way_point_id + 1) % _way_points.Count;
            }
        }
    }
}
