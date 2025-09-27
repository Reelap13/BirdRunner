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

        private SplineContainer _container;
        private float _total_length;
        private float _distance;

        public void Initialize(SplineContainer container, float total_length, float distance)
        {
            _container = container;
            _total_length = total_length;
            _distance = distance;

            OnInitialized.Invoke();
        }

    }
}