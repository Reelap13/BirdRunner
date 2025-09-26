using UnityEngine;

namespace Game.Level.Constructor.Curve
{
    [CreateAssetMenu(fileName = "Curve", menuName = "Game/Level/Constructor/Curve")]
    public class CurvePreset : ScriptableObject
    {
        public float Length = 1;
        public float Steps = 2;
    }
}