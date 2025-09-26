using UnityEngine;

namespace Game.Level.Constructor.Curve
{
    public class StraightLineCurve : CurveFunction
    {
        public override Vector3 GetPoint(float length)
        {
            return new Vector3(0, 0, length);
        }
    }
}