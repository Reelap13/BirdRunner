using System;
using Mirror;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Splines;

namespace Game.Level.Obstacles
{
    public class ObstacleController : NetworkBehaviour
    {
        [NonSerialized] public UnityEvent OnInitialized = new();

        private ObstaclesPreset _preset;
        private SplineContainer _container;
        private float _total_length;
        private float _distance;

        public void Initialize(SplineContainer container, ObstaclesPreset preset, float total_length, float distance)
        {
            _container = container;
            _preset = preset;
            _total_length = total_length;
            _distance = distance;

            OnInitialized.Invoke();
        }

        public void EditorInitialize(ObstaclesPreset preset)
        {
            _preset = preset;
        }

        public void Evaluate(float distance_offset, out Vector3 position, out Quaternion rotation)
        { 
            float t = math.clamp(_distance - distance_offset, 0, _total_length) / _total_length;
            SplineUtility.Evaluate(_container.Spline, t,
                out float3 pos, out float3 tangent, out float3 up);

            position = pos;
            rotation = Quaternion.LookRotation(tangent, up);
        }

        public ObstaclesPreset Preset { get { return _preset; } }
    }
}