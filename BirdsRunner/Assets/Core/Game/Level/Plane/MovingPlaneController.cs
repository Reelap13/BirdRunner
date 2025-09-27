using UnityEngine;
using Mirror;
using UnityEngine.Splines;
using Unity.Mathematics;


public class MovingPlaneController : NetworkBehaviour
{
    [SerializeField] private float planeSpeed = 10f;

    private SplineContainer _container;
    private float currentDistance;


    void Update()
    {
        if (!isServer) return;
        if (_container == null) return;
        currentDistance += planeSpeed * Time.deltaTime;
        var spline = _container.Spline;

        float4x4 local_to_world = _container.transform.localToWorldMatrix;
        float total_length = SplineUtility.CalculateLength(spline, local_to_world);

        float distance = Mathf.Clamp(currentDistance, 0, total_length);
        float t = distance / total_length;

        SplineUtility.Evaluate(spline, t,
            out float3 pos, out float3 tangent, out float3 up);

        Quaternion rot = Quaternion.LookRotation(tangent, up);

        transform.position = pos;
        transform.rotation = rot;
    }

    public void SetSpline(SplineContainer container)
    {
        _container = container;
    }
}
