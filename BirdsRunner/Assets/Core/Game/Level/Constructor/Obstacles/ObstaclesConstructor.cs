using System.Collections.Generic;
using Game.Level.Constructor.Tube;
using Game.Level.Obstacles;
using Mirror;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.Splines;
using UnityEngine.Events;
using System;
using UnityEditor;

namespace Game.Level.Constructor.Obstacles
{
    public class ObstaclesConstructor : MonoBehaviour
    {
        [SerializeField] private SplineContainer _container;
        [SerializeField] private TubeConstructor _tube_constructor;
        [SerializeField] private List<ObstacleCreatingData> _obstacles;

        private List<ObstacleController> _spawned = new();

        [NonSerialized] public UnityEvent OnObstaclesPlaced = new();

        public void Generate(bool is_editor = false)
        {
            DestroySpawnedObjects(is_editor);
            CreateObstacles(is_editor);
        }

        public void DestroySpawnedObjects(bool is_editor)
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

                Vector3 forward = new Vector3(tangent.x, tangent.y, tangent.z).normalized;
                Vector3 right = Vector3.Cross(up, forward).normalized;
                Vector3 real_up = Vector3.Cross(forward, right).normalized;

                float radius = _tube_constructor.TubePreset.Radius;
                Vector3 position = new Vector3(pos.x, pos.y, pos.z) +
                    right * (data.Offset.x * radius) + real_up * (data.Offset.y * radius);

                Quaternion baseRotation = Quaternion.LookRotation(forward, real_up);
                Quaternion twist = Quaternion.AngleAxis(data.Rotation * 360f, forward);
                Quaternion rotation = twist * baseRotation;

                var obstacle = CreateObstacle(data.Preset, position, rotation, distance, total_length, is_editor);
                _spawned.Add(obstacle);
            }
        }


        public void UpdateObstaclesFromChildren()
        {
            _obstacles.Clear();

            var spline = _container.Spline;
            float4x4 local_to_world = _container.transform.localToWorldMatrix;
            float total_length = SplineUtility.CalculateLength(spline, local_to_world);

            for (int i = 0; i < transform.childCount; i++)
            {
                var child = transform.GetChild(i);
                var obstacle = child.GetComponent<ObstacleController>();
                if (obstacle == null) continue;

                float3 pos = child.position;
                SplineUtility.GetNearestPoint(spline, pos, out float3 nearest, out float t);
                float distance = total_length * t;

                SplineUtility.Evaluate(spline, t, out float3 splinePos, out float3 tangent, out float3 up);
                Vector3 forward = new Vector3(tangent.x, tangent.y, tangent.z).normalized;
                Vector3 right = Vector3.Cross(up, forward).normalized;
                Vector3 real_up = Vector3.Cross(forward, right).normalized;

                Vector3 localOffset = child.position - (Vector3)splinePos;
                float radius = _tube_constructor.TubePreset.Radius;
                float offsetX = Vector3.Dot(localOffset, right) / radius;
                float offsetY = Vector3.Dot(localOffset, real_up) / radius;

                Quaternion baseRotation = Quaternion.LookRotation(forward, real_up);
                Quaternion delta = Quaternion.Inverse(baseRotation) * child.rotation;
                float twist = delta.eulerAngles.z / 360f; // нормализуем в [0,1]

                var preset = obstacle.Preset;

                _obstacles.Add(new ObstacleCreatingData()
                {
                    Preset = preset,
                    SpawnDistance = distance,
                    Offset = new Vector2(offsetX, offsetY),
                    Rotation = twist
                });
            }

#if UNITY_EDITOR
            EditorUtility.SetDirty(this);
#endif
        }

        private ObstacleController CreateObstacle(ObstaclesPreset preset, Vector3 pos, Quaternion rot, float distance, float total_length, bool is_editor)
        {
            ObstacleController obstacle = null;
            if (is_editor)
            {
                obstacle = Instantiate(preset.Prefab, pos, rot, transform);
                obstacle.EditorInitialize(preset);
            }
            else
            {
                obstacle = NetworkUtils.NetworkInstantiate(preset.Prefab, pos, rot, transform);
                obstacle.Initialize(_container, preset, total_length, distance);
            }
            return obstacle;
        }

        public List<ObstacleController> Spawned { get => _spawned; private set { } }
    }
}