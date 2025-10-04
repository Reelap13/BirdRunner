using UnityEngine;
using Mirror;
using UnityEngine.Splines;
using Unity.Mathematics;
using Game.Sound;
using Game.Level.Constructor.Plane;
using System.Collections.Generic;

public class MovingPlaneController : NetworkBehaviour
{
    [SerializeField] private float planeSpeed = 10f;
    [SerializeField] private float smoothingFactor = 0.1f; // Adjust in Inspector
    [SerializeField] private SoundPlayer music;
    [SerializeField] private MusicLibrary musicLibrary;

    private SplineContainer _container;
    private float currentDistance;

    private Vector3 _targetPosition; // Store target position
    private Quaternion _targetRotation; // Store target rotation


    void Update()
    {
        if (!isServer) return;
        if (_container == null) return;

        // Calculate the target position and rotation in Update
        CalculateTargetPositionAndRotation();
    }

    void FixedUpdate()
    {
        if (!isServer) return;
        if (_container == null) return;

        // Smoothly interpolate towards the target position and rotation
        transform.position = Vector3.Lerp(transform.position, _targetPosition, smoothingFactor);
        transform.rotation = Quaternion.Slerp(transform.rotation, _targetRotation, smoothingFactor);
    }


    private void CalculateTargetPositionAndRotation()
    {
        currentDistance += planeSpeed * Time.deltaTime;
        var spline = _container.Spline;

        float4x4 local_to_world = _container.transform.localToWorldMatrix;
        float total_length = SplineUtility.CalculateLength(spline, local_to_world);

        float distance = Mathf.Clamp(currentDistance, 0, total_length);
        float t = distance / total_length;

        SplineUtility.Evaluate(spline, t,
            out float3 pos, out float3 tangent, out float3 up);

        Quaternion rot = Quaternion.LookRotation(tangent, up);

        _targetPosition = pos;  // Store the calculated position
        _targetRotation = rot;  // Store the calculated rotation
    }

    public void SetSpline(SplineContainer container)
    {
        _container = container;
    }

    [ClientRpc]
    public void SetMusic(int clipIndex)
    {
        AudioClip clip = musicLibrary.GetAudioClip(clipIndex);
        music.SetMusic(clip);
        music.PlaySound(true);
    }

}
