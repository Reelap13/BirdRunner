using UnityEngine;
using Mirror;
using UnityEngine.Events;
using System;

namespace Game.GameMode {

    public class GameModeChanger : NetworkBehaviour
    {
        [SerializeField] private GameModeTypes gameMode;
        private bool isTriggered;

        [NonSerialized] public UnityEvent<GameModeTypes> OnGameModeChanged = new();

        private void OnTriggerEnter(Collider other)
        {
            if (!isServer)
                return;
            if (isTriggered) return;

            OnGameModeChanged.Invoke(gameMode);

        }
    }
}
