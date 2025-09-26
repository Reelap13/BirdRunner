using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Splines;

namespace Game.Level.Constructor.Obstacles
{
    public class ObstaclesConstructor : MonoBehaviour
    {
        [SerializeField] private SplineContainer _container;
        [SerializeField] private List<ObstacleCreatingData> _obstacles;

        private List<GameObject> _spawned = new List<GameObject>();

        public void Generate()
        {
            DestroySpawnedObjects();
            CreateObstacles();
        }

        private void DestroySpawnedObjects()
        {
            foreach (var obstacle in _spawned)
                DestroyImmediate(obstacle);
            _spawned.Clear();
        }

        private void CreateObstacles()
        {
            var spline = _container.Spline;

            float4x4 local_to_world = _container.transform.localToWorldMatrix;
            float total_length = SplineUtility.CalculateLength(spline, local_to_world);

            foreach (var data in _obstacles)
            {
                float distance = Mathf.Clamp(data.SpawnDistance, 0, total_length);
                float t = distance / total_length;

                SplineUtility.Evaluate(spline, t,
                    out float3 pos, out float3 tangent, out float3 up);

                Quaternion rot = Quaternion.LookRotation(tangent, up);

                var instance = Instantiate(data.Preset.Prefab, pos, rot, transform);
                _spawned.Add(instance);
            }
        }
    }
}