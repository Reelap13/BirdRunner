using UnityEngine;


namespace Game.Level.Obstacles.Triggers
{
    public class BladeTrigger : TriggerController
    {
        [SerializeField] private Trigger _trigger;
        protected override void Initialize()
        {
            _trigger.OnTriggered.AddListener((_) => ProcessTriggering());
        }

    }
}