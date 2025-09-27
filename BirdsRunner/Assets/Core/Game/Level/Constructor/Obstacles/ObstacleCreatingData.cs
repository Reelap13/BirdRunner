using Game.Level.Obstacles;
using UnityEngine;

namespace Game.Level.Constructor.Obstacles
{
    [System.Serializable]
    public class ObstacleCreatingData
    {
        public ObstaclesPreset Preset;
        public float SpawnDistance;
        public Vector2 Offset;
        public float Rotation;
    }
}