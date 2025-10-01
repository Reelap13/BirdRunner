using Game.Level.Obstacles.Triggers;
using Mirror;
using UnityEngine;

namespace Game.Level.Obstacles
{
    public abstract class ObstacleBehaviour : NetworkBehaviour
    {
        [SerializeField] protected ObstacleController _obstacle;
        [SerializeField] private TriggerController _trigger;

        public override void OnStartServer() => _trigger.OnTriggered.AddListener(ActivateBehaviour);

        protected abstract void ActivateBehaviour();
    }
}