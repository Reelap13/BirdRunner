using Game.Level.Constructor;
using UnityEngine;

namespace Game.Level
{
    [CreateAssetMenu(fileName = "LevelPreset", menuName = "Game/Level/Preset")]
    public class LevelPreset : ScriptableObject
    {
        public int LevelId;
        public LevelController Prefab;
    }
}