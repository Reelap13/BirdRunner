using UnityEngine;
using Game.PlayerSide.Character;

namespace Game.Level.Obstacles
{
    public class TeleportTrigger : ObstacleBehaviour
    {
        [SerializeField] private Transform firstPoint;
        [SerializeField] private Transform secondPoint;

        protected override void ActivateBehaviour()
        {
            PlayerCharacterController[] birds = FindObjectsByType<PlayerCharacterController>(FindObjectsSortMode.None);
            if (birds.Length != 2) return;
            if(Random.value > 0.5f)
            {
                birds[0].transform.position = firstPoint.position;
                birds[1].transform.position = secondPoint.position;
            }
            else
            {
                birds[1].transform.position = firstPoint.position;
                birds[0].transform.position = secondPoint.position;
            }
        }

    }
}
