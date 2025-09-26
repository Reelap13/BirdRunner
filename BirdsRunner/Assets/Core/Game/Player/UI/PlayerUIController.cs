using Mirror;
using UnityEngine;

namespace Game.PlayerSide
{
    public class PlayerUIController : NetworkBehaviour
    {
        [field: SerializeField]
        public PlayerController PlayerController { get; private set; }

        private void Awake()
        {
            gameObject.SetActive(false);
            PlayerController.OnClientInitialized.AddListener(Initialize);
        }

        private void Initialize()
        {
            gameObject.SetActive(true);
        }
    }
}