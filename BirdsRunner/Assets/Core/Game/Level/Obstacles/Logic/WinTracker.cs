using UnityEngine;

namespace Game.Level.Obstacles
{
    public class WinTracker : ObstacleBehaviour
    {
        protected override void ActivateBehaviour()
        {
            GameController.Instance.Race.Tracker.RegisterWin();
        }
    }
}