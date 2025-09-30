using System.Collections.Generic;
using Game.Level.Obstacles;
using Mirror;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Splines;

namespace Game.Level.Constructor.Plane
{
    public class MovingPlaneConstructor : MonoBehaviour
    {
        [SerializeField] private SplineContainer _container;
        [SerializeField] private GameObject planePrefab;


        public void Generate()
        {
            MovingPlaneController plane = FindFirstObjectByType<MovingPlaneController>();
            if(plane != null)
            {
                NetworkServer.Destroy(plane.gameObject);
            }
            var spline = _container.Spline;

            SplineUtility.Evaluate(spline, 0,
                out float3 pos, out float3 tangent, out float3 up);

            Quaternion rot = Quaternion.LookRotation(tangent, up);

            GameObject newPlane = NetworkUtils.NetworkInstantiate(planePrefab, pos, rot, null);

            newPlane.GetComponent<MovingPlaneController>().SetSpline(_container);

        }
    }
}
