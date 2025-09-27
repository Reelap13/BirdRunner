using System.Collections.Generic;
using Game.Level.Obstacles;
using Mirror;
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

        public void Generate(bool is_editor = false)
        {
            DestroySpawnedObjects(is_editor);
            CreateObstacles(is_editor);
        }

        private void DestroySpawnedObjects(bool is_editor)
        {
            for (int i = transform.childCount - 1; i >= 0; i--)
            {
                var child = transform.GetChild(i).gameObject;
                if (is_editor)                
                    DestroyImmediate(child);
                else NetworkServer.Destroy(child);
            }
            _spawned.Clear();
        }

        private void CreateObstacles(bool is_editor)
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

                var obstacle = CreateObstacle(data.Preset, pos, rot, is_editor);
                _spawned.Add(obstacle);
            }
        }

        private GameObject CreateObstacle(ObstaclesPreset preset, float3 pos, Quaternion rot, bool is_editor)
        {
            GameObject obstacle = null;
            if (is_editor) obstacle = Instantiate(preset.Prefab, pos, rot, transform);
            else obstacle = NetworkUtils.NetworkInstantiate(preset.Prefab, pos, rot, transform);
            return obstacle;
        }
    }
}