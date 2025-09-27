using System;
using System.Collections.Generic;
using Server;
using Server.ServerSide;
using UnityEngine;
using UnityEngine.Events;
using static Mirror.SimpleWeb.Log;

namespace Game.Level
{
    public class LevelLoader : GameSystem
    {
        [NonSerialized] public UnityEvent OnLoaded = new();
        [NonSerialized] public UnityEvent OnReloaded = new();

        [SerializeField] private List<LevelPreset> _presets;

        private LevelController _level;

        protected override void Initialize()
        {
            GameController.Instance.OnStarted.AddListener(LoadLevel);
            GameController.Instance.OnRestarted.AddListener(ReloadLevel);
        }

        private void LoadLevel()
        {
            DestroyLevel();
            CreateLevel(LevelId);
            OnLoaded.Invoke();
        }

        private void ReloadLevel()
        {
            DestroyLevel();
            CreateLevel(LevelId);
            OnReloaded.Invoke(); 
        }

        private void DestroyLevel()
        {
            if (_level == null) return;

            Destroy(_level.gameObject);
            _level = null;
        }

        private void CreateLevel(int level_id)
        {
            LevelPreset preset = FindLevel(level_id);
            if (preset == null)
            {
                Debug.LogError($"Error: try to load preset with non-exsisted id {level_id}");
                return;
            }

            _level = NetworkUtils.NetworkInstantiate(preset.Prefab, transform, transform);
        }

        private LevelPreset FindLevel(int level_id)
        {
            foreach (var preset in _presets)
                if (level_id == preset.LevelId) return preset;
            return null;
        }

        private int LevelId = 0;
        public LevelController Level { get { return _level; } }
    }
}