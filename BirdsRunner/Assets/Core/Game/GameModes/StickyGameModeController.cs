using UnityEngine;
using Game.PlayerSide.Character;

namespace Game.GameMode
{
    public class StickyGameModeController : GameModeController
    {
        [SerializeField] private float birdHeight;
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
            if (Random.value > 0.5f)
            {
                serverPlayer.GetComponent<CharacterMovement>().ActivateStickyFeathers(true, clientPlayer.GetComponent<CharacterMovement>());
                clientPlayer.GetComponent<CharacterMovement>().ActivateStickyFeathers(false, serverPlayer.GetComponent<CharacterMovement>());
                serverPlayer.transform.position = clientPlayer.transform.position + Vector3.up * birdHeight;
            }
            else
            {
                serverPlayer.GetComponent<CharacterMovement>().ActivateStickyFeathers(false, clientPlayer.GetComponent<CharacterMovement>());
                clientPlayer.GetComponent<CharacterMovement>().ActivateStickyFeathers(true, serverPlayer.GetComponent<CharacterMovement>());
                clientPlayer.transform.position = serverPlayer.transform.position + Vector3.up * birdHeight;
            }

        }

        public override void DiactivateGameMode()
        {
            foreach (var player in controller.Game.Players.Players)
            {
                player.GetComponent<CharacterMovement>().DiactivateStickyFeathers();
            }
        }
    }
}
