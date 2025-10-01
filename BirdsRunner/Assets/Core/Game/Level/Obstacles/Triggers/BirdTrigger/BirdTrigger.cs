using UnityEngine;


namespace Game.Level.Obstacles.Triggers
{
    public class BirdTrigger : TriggerController
    {
        [SerializeField] private float _distance_offset = 15f;
        [SerializeField] private Trigger _trigger_prefab;

        private Trigger _trigger;

        protected override void Initialize()
        {
            CreateTrigger();
            _trigger.OnTriggered.AddListener((_) => ProcessTriggering());
        }

        private void CreateTrigger()
        {
            _obstacle.Evaluate(_distance_offset, out Vector3 pos, out Quaternion rot);
            _trigger = Instantiate(_trigger_prefab, pos, rot, transform);
        }
    }
}