using Game.Level.Constructor.Tube;
using Mirror;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Splines;

namespace Game.Level.Constructor.TubeRings
{
    public class TubeRingsContructor : MonoBehaviour
    {
        [SerializeField] private SplineContainer _container;

        [Header("Ring settings")]
        [SerializeField] private GameObject _ringPrefab;
        [SerializeField] private float _ringSpacing = 10f; 
        [SerializeField] private float _startOffset = 0f;
        [SerializeField] private bool _generateInEditor = true;

        private List<GameObject> _spawnedRings = new();

        public void GenerateRings(bool isEditor = false)
        {
            DestroyRings(isEditor);

            if (_ringPrefab == null || _container == null) return;

            var spline = _container.Spline;
            float4x4 localToWorld = _container.transform.localToWorldMatrix;
            float totalLength = SplineUtility.CalculateLength(spline, localToWorld);

            for (float dist = _startOffset; dist <= totalLength; dist += _ringSpacing)
            {
                float t = dist / totalLength;

                SplineUtility.Evaluate(spline, t, out float3 pos, out float3 tangent, out float3 up);

                Vector3 forward = ((Vector3)tangent).normalized;
                Vector3 right = Vector3.Cross(up, forward).normalized;
                Vector3 realUp = Vector3.Cross(forward, right).normalized;

                Vector3 position = pos;

                Quaternion rotation = Quaternion.LookRotation(forward, realUp);

                GameObject ring;

#if UNITY_EDITOR
                if (isEditor && _generateInEditor)
                    ring = (GameObject)UnityEditor.PrefabUtility.InstantiatePrefab(_ringPrefab, transform);
                else
                    ring = Instantiate(_ringPrefab, transform);
#else
                ring = Instantiate(_ringPrefab, transform);
#endif

                ring.transform.position = position;
                ring.transform.rotation = rotation;
                _spawnedRings.Add(ring);
            }
        }

        public void DestroyRings(bool is_editor)
        {
            for (int i = transform.childCount - 1; i >= 0; i--)
            {
                var child = transform.GetChild(i).gameObject;
                if (is_editor)
                    DestroyImmediate(child);
                else Destroy(child);
            }
            _spawnedRings.Clear();
        }
    }
}