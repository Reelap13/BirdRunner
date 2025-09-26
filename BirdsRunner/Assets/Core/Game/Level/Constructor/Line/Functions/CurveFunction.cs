using UnityEngine;

namespace Game.Level.Constructor.Curve
{
    public abstract class CurveFunction : MonoBehaviour
    {
        public abstract Vector3 GetPoint(float length);
    }
}