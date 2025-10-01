using System;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Level.Obstacles.Triggers
{
    public abstract class TriggerController : MonoBehaviour
    {
        [NonSerialized] public UnityEvent OnTriggered = new();

        [SerializeField] protected ObstacleController _obstacle;

        private bool _is_triggered = false;

        private void Awake()
        {
            _obstacle.OnInitialized.AddListener(Initialize);
        }

        protected virtual void Initialize() { }

        protected void ProcessTriggering()
        {
            if (_is_triggered)
                return;

            _is_triggered = true;
            OnTriggered.Invoke();
        }

        public bool IsTriggered { get { return _is_triggered; } }
    }
}