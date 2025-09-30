using UnityEngine;
using Game.PlayerSide.Character;

namespace Game.GameMode
{
    public class RopeGameModeController : GameModeController
    {
        public override void ActivateGameMode()
        {
            PlayerCharacterController serverPlayer = null;
            PlayerCharacterController clientPlayer = null;
            foreach (var player in controller.Game.Players.Players)
            {
                
                if (player.isOwned)
                {
                    serverPlayer = player.CharacterCreator.Character;
                }
                else
                {
                    clientPlayer = player.CharacterCreator.Character;
                }
            }
            if (serverPlayer == null || clientPlayer == null) return;
            serverPlayer.GetComponent<CharacterMovement>().ActivateRope(clientPlayer.transform);
            clientPlayer.GetComponent<CharacterMovement>().ActivateRope(serverPlayer.transform);
        }

        public override void DiactivateGameMode()
        {
            foreach (var player in controller.Game.Players.Players)
            {
                player.GetComponent<CharacterMovement>().DiactivateRope();
            }
        }
    }
}
