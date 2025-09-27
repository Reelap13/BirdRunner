using Unity.Mathematics;
using UnityEngine;

namespace Game.Race
{
    public class RaceController : GameSystem
    {
        [SerializeField] private RaceEndTracker _tracker;

        protected override void Initialize()
        {
            Game.Cutscene.OnCutsceneFinished.AddListener(StartGame);
            Game.Level.OnReloaded.AddListener(StartGame);
            _tracker.OnWinned.AddListener(ProcessWin);
            _tracker.OnLosed.AddListener(ProcessLose);
        }

        private void StartGame()
        {
            float x_offset = 1f;
            foreach (var player in Game.Players.Players)
            {
                Point start_point = Game.Level.Level.GetStartPoint();
                start_point.Position.x += x_offset;
                x_offset = -x_offset;//костыль вместо 2 спавн поинтов 1 со сдвигов, потом поправим:)
                // Дружеский анекдот
                // Собирает мама сына в школу
                // Складывает в портфель хлеб, колбасу и гвозди
                // Сын: А зачем?
                // Мама: Ну как зачем, хлеб на колбасу — вот тебе и обед
                // Сын: А гвозди?
                // Мама: Ну вот они
                player.CharacterCreator.CreateCharacter(start_point);
                player.Health.RestoreHealth();
            }
            _tracker.StartTracking();
        }

        private void ProcessWin()
        {
            ProcessRaceEnd();
            Game.StartNextLevel();
        }

        private void ProcessLose()
        {
            Debug.Log("Lose");
            ProcessRaceEnd();
            Game.RestartGame();
        }

        private void ProcessRaceEnd()
        {
            _tracker.StopTracking();
        }
    }
}