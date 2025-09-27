using UnityEngine;

namespace Game.Race
{
    public class RaceController : GameSystem
    {
        protected override void Initialize()
        {
            Game.Cutscene.OnCutsceneFinished.AddListener(StartGame);
        }

        private void StartGame()
        {
            Point start_point = Game.Level.Level.GetStartPoint();
            foreach (var player in Game.Players.Players)
            {
                player.CharacterCreator.CreateCharacter(start_point);
                player.Health.RestoreHealth();
            }
        }
    }
}