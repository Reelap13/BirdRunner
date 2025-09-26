using Game.Level;
using Server.Lobby;
using Server.ServerSide;
using Unity.VisualScripting;
using UnityEngine;

namespace Game
{
    public class GameInitializer : MonoBehaviour
    {
        [field: SerializeField]
        public GamePlayersController PlayersController { get; private set; }
        [field: SerializeField]
        public LevelCreator LevelCreator { get; private set; }
        [field: SerializeField]
        public GameController GameController { get; private set; }


        public void Initialize(LobbyData data)
        {
            PlayersController.CreatePlayersControllers(data);

            LevelCreator.OnLevelInitializationEnded.AddListener(FinishGameInitialization);
            LevelCreator.CreateLevel();
        }

        private void FinishGameInitialization()
        {
            GameController.StartGame();
        }
    }
}