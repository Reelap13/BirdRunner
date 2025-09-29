using UnityEngine;
using System.Collections.Generic;

namespace Game.GameMode
{
    public enum GameModeTypes
    {
        Default,
        Separation,
        Magnetism,
        Rope,
        Sticky
    }
    public class GameModesController : GameSystem
    {
        [SerializeField] private DefaultGameModeController defaultGameMode;
        [SerializeField] private SeparationGameModeController separationGameMode;
        [SerializeField] private MagnetismGameModeController magnetismGameMode;
        [SerializeField] private RopeGameModeController ropeGameMode;
        [SerializeField] private StickyGameModeController stickyGameMode;

        private GameModeTypes currentGameMode;
        private GameModeController currentController;

        protected override void Initialize()
        {
            Game.Level.OnLoaded.AddListener(RegisterObstacles);
            Game.Level.OnReloaded.AddListener(RegisterObstacles);
            currentGameMode = GameModeTypes.Default;
            currentController = defaultGameMode;
            currentController.ActivateGameMode();
            
        }

        private void RegisterObstacles()
        {
            List<GameObject> obstacles = Game.Level.Level.LevelConstructor.Obstacles.Spawned;

            foreach (var item in obstacles)
            {
                if(item.TryGetComponent(out GameModeChanger gameModeChanger))
                {
                    gameModeChanger.OnGameModeChanged.AddListener(ChangeGameMode);
                }
            }
        }

        private void ChangeGameMode(GameModeTypes type)
        {
            currentGameMode = type;
            currentController.DiactivateGameMode();
            switch (type)
            {
                case (GameModeTypes.Default):
                    currentController = defaultGameMode;
                    break;
                case (GameModeTypes.Separation):
                    currentController = separationGameMode;
                    break;
                case (GameModeTypes.Magnetism):
                    currentController = magnetismGameMode;
                    break;
                case (GameModeTypes.Rope):
                    currentController = ropeGameMode;
                    break;
                case (GameModeTypes.Sticky):
                    currentController = stickyGameMode;
                    break;
                default:
                    break;
            }
            currentController.ActivateGameMode();
        }

        public GameModeTypes GetGameMode()
        {
            return currentGameMode;
        }

    }
}
