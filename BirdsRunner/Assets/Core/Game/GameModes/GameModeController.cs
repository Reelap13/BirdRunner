using UnityEngine;

namespace Game.GameMode
{
    public abstract class GameModeController : MonoBehaviour
    {
        [SerializeField] protected GameModesController controller;
        public abstract void ActivateGameMode();

        public abstract void DiactivateGameMode();
    }
}
