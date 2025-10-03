using System;
using Game.Cutscene;
using Game.Level;
using Game.Race;
using UnityEngine;
using UnityEngine.Events;
using Game.GameMode;

namespace Game
{
    public class GameController : Singleton<GameController>
    {
        [NonSerialized] public UnityEvent OnInitialized = new();
        [NonSerialized] public UnityEvent OnStarted = new();
        [NonSerialized] public UnityEvent OnRestarted = new();

        [field: SerializeField]
        public GamePlayersController Players { get; private set; }
        [field: SerializeField]
        public LevelLoader Level { get; private set; }
        [field: SerializeField]
        public CutsceneController Cutscene { get; private set; }
        [field: SerializeField]
        public RaceController Race { get; private set; }
        [field: SerializeField]
        public GameModesController GameModes { get; private set; }

        private string SAVE_KEY = "LEVEL_ID";
        public int LevelId { get; private set; }

        public void Initialize()
        {
            LevelId = PlayerPrefs.GetInt(SAVE_KEY);
            OnInitialized.Invoke();
        }
        public void StartGame() => OnStarted.Invoke();
        public void RestartGame() => OnRestarted.Invoke();
        public void StartNextLevel()
        {
            ++LevelId;
            if (LevelId == 5)
                LevelId = 1;
            PlayerPrefs.SetInt(SAVE_KEY, LevelId);
            StartGame();
        }
    }
}