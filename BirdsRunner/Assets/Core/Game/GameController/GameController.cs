using System;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class GameController : Singleton<GameController>
    {
        [NonSerialized] public UnityEvent OnNewGameStarted = new();

        [field: SerializeField]
        public SavesController SavesController { get; private set; }

        public void StartGame()
        {
            if (SavesController.IsHasSave())
                SavesController.LoadGame();
            else OnNewGameStarted.Invoke();
        }

        public void RestartGame()
        {
            StartGame();
        }
    }
}