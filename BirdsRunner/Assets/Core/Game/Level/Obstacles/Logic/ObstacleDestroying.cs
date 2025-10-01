using UnityEngine;

namespace Game.Level.Obstacles
{
    public class ObstacleDestroying : ObstacleBehaviour
    {
        [SerializeField] private GameObject _model;

        protected override void ActivateBehaviour()
        {
            _model.SetActive(false);
        }
    }
}