using System;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Race
{
    public class RaceEndTracker : MonoBehaviour
    {
        [NonSerialized] public UnityEvent OnWinned = new();
        [NonSerialized] public UnityEvent OnLosed = new();

        public void StartTracking()
        {
            foreach (var player in GameController.Instance.Players.Players)
                player.Health.OnDied.AddListener(RegisterLose);
        }

        public void StopTracking()
        {
            foreach (var player in GameController.Instance.Players.Players)
                player.Health.OnDied.RemoveListener(RegisterLose);
        }

        private void RegisterLose()
        {
            OnLosed.Invoke();
        }

        public void RegisterWin()
        {
            OnWinned.Invoke();
        }
    }
}