using System.Collections;
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
        public GameController GameController { get; private set; }


        public void Initialize(LobbyData data)
        {
            PlayersController.CreatePlayersControllers(data);
            StartCoroutine(FinishGameInitialization());
        }

        private IEnumerator FinishGameInitialization()
        {
            yield return null;
            GameController.Initialize();
            GameController.StartGame();
        }
    }
}