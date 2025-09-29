using UnityEngine;

namespace Game.GameMode
{
    public class MagnetismGameModeController : GameModeController
    {
        public override void ActivateGameMode()
        {
            foreach (var player in controller.Game.Players.Players)
            {
                player.CharacterCreator.Character.GetComponent<PlayerMagneticFieldController>().ActivateField();
            }
        }

        public override void DiactivateGameMode()
        {
            foreach (var player in controller.Game.Players.Players)
            {
                player.CharacterCreator.Character.GetComponent<PlayerMagneticFieldController>().DiactivateField();
            }
        }
    }
}
