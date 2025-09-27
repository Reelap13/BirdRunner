using UnityEngine;

namespace Game
{
    public abstract class GameSystem : MonoBehaviour
    {
        public GameController Game => GameController.Instance;

        private void Awake()
        {
            Game.OnInitialized.AddListener(Initialize);
        }

        protected virtual void Initialize() { }
    }
}