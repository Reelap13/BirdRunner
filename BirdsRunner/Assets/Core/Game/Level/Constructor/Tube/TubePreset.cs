using UnityEngine;

namespace Game.Level.Constructor.Tube
{
    [CreateAssetMenu(fileName = "Tube", menuName = "Game/Level/Constructor/Tube")]
    public class TubePreset : ScriptableObject
    {
        public float SegmentLength = 0.5f;
        public int RadialSegments = 16;
        public float Radius = 1f; 
    }
}