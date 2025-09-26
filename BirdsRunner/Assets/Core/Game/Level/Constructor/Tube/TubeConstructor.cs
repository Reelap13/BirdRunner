using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem.iOS;
using UnityEngine.Splines;

namespace Game.Level.Constructor.Tube
{
    public class TubeConstructor : MonoBehaviour
    {
        [SerializeField] private SplineContainer _container;
        [SerializeField] private TubePreset _preset;
        [SerializeField] private Material _material;

        public void Generate()
        {
            var spline = _container.Spline;

            float4x4 local_to_world = _container.transform.localToWorldMatrix;
            float total_length = SplineUtility.CalculateLength(spline, local_to_world);

            int length_segments = Mathf.Max(2, Mathf.CeilToInt(total_length / _preset.SegmentLength));

            int radial = _preset.RadialSegments;
            Vector3[] vertices = new Vector3[(radial + 1) * (length_segments + 1)];
            Vector2[] uvs = new Vector2[vertices.Length];
            int[] triangles = new int[radial * length_segments * 6];

            int vert_index = 0;
            int tri_index = 0;

            for (int i = 0; i <= length_segments; ++i)
            {
                float t = (float)i / length_segments;
                SplineUtility.Evaluate(spline, t, out float3 pos, out float3 tangent, out float3 up);

                Quaternion rot = Quaternion.LookRotation(tangent, up);

                for (int j = 0; j <= radial; ++j)
                {
                    float angle = (float)j / radial * Mathf.PI * 2f;
                    Vector3 circle = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * _preset.Radius;
                    vertices[vert_index] = new Vector3(pos.x, pos.y, pos.z) + rot * circle;
                    uvs[vert_index] = new Vector2((float)j / radial, t);
                    ++vert_index;
                }
            }

            for (int i = 0; i < length_segments; ++i)
            {
                for (int j = 0; j < radial; ++j)
                {
                    int a = i * (radial + 1) + j;
                    int b = a + radial + 1;

                    triangles[tri_index++] = a;
                    triangles[tri_index++] = b;
                    triangles[tri_index++] = a + 1;

                    triangles[tri_index++] = b;
                    triangles[tri_index++] = b + 1;
                    triangles[tri_index++] = a + 1;
                }
            }

            Mesh mesh = new Mesh();
            mesh.vertices = vertices;
            mesh.triangles = triangles;
            mesh.uv = uvs;
            mesh.RecalculateNormals();

            var mesh_filter = GetComponent<MeshFilter>();
            mesh_filter.mesh = mesh;

            var mesh_collider = GetComponent<MeshCollider>();
            mesh_collider.sharedMesh = mesh;

            var meshRenderer = GetComponent<MeshRenderer>();
            meshRenderer.material = _material;
        }
    }
}