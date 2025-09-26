using System.ComponentModel;
using UnityEngine;
using UnityEngine.Splines;

namespace Game.Level.Constructor.Line
{
    public class CurveConstructor : MonoBehaviour
    {
        [SerializeField] private CurvePreset _preset;
        [SerializeField] private CurveFunction _function;
        [SerializeField] private SplineContainer _container;

        public void GenerateLine()
        {
            float step_length = _preset.Length / (_preset.Steps - 1);

            _container.Spline.Clear();
            _container.Spline.Closed = false;

            for (int i = 0; i < _preset.Steps; ++i)
            {
                float s = step_length * i;
                Vector3 point = _function.GetPoint(s);
                _container.Spline.Add(point);
            }
        }
    }
}