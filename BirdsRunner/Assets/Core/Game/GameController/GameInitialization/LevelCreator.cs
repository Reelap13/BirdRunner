using System;
using System.Collections;
using Game.Level;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class LevelCreator : MonoBehaviour
    {
        [NonSerialized] public UnityEvent OnLevelInitializationEnded = new();

        [SerializeField] private LevelController _level_prefab;

        private LevelController _level;

        public void CreateLevel()
        {
            _level = NetworkUtils.NetworkInstantiate(_level_prefab);
            _level.Initialize();
            StartCoroutine(SimulateLevelLoadingProcess());
        }

        private IEnumerator SimulateLevelLoadingProcess()
        {
            yield return null;
            OnLevelInitializationEnded.Invoke();
        }

        public LevelController Level => _level;
    }
}