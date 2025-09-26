using UnityEngine;

namespace Game.Level.Obstacles
{
    [CreateAssetMenu(fileName = "Obstacle", menuName = "Game/Level/Obstacle")]
    public class ObstaclesPreset : ScriptableObject
    {
        public GameObject Prefab;
    }
}