using UnityEngine;
using Mirror;
using System.Collections.Generic;

namespace Game.PlayerSide.Character
{
    public class PlayerColorController : NetworkBehaviour
    {
        [SyncVar(hook = nameof(OnPlayerColorChanged))]
        public Color playerColor = Color.white;

        public List<Renderer> playerRenderers;

        void Start()
        {
            if (playerRenderers == null)
            {
                Debug.LogError("Player Renderer not assigned!  Please assign it in the Inspector.");
                enabled = false;
                return;
            }

            if (isServer)
            {
                InitializePlayerColor(Random.ColorHSV());
            }
        }

        [Server]
        public void InitializePlayerColor(Color newColor)
        {
            playerColor = newColor;
        }

        void OnPlayerColorChanged(Color oldColor, Color newColor)
        {
            foreach (var item in playerRenderers)
            {
                Material playerMaterial = item.material;
                playerMaterial.color = newColor;
            }
        }

        [Command]
        public void CmdChangeColor(Color newColor)
        {
            playerColor = newColor;
        }

        public void RequestColorChange(Color newColor)
        {
            CmdChangeColor(newColor);
        }
    }
}
