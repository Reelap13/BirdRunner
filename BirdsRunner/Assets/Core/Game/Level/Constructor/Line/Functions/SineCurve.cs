using UnityEngine;

namespace Game.Level.Constructor.Line
{
    public class SineCurve : CurveFunction
    {
        [SerializeField] private float _amplitude = 3f;
        [SerializeField] private float _frequency = 0.1f;

        public override Vector3 GetPoint(float length)
        {
            float x = Mathf.Sin(length * _frequency) * _amplitude;
            return new Vector3(x, 0, length);
        }
    }
}