#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Game.Level.Constructor.Curve
{

    [CustomEditor(typeof(CurveConstructor))]
    public class CurveConstructorEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            CurveConstructor gen = (CurveConstructor)target;
            if (GUILayout.Button("Generate Spline"))
            {
                gen.Generate();
            }
        }
    }
}
#endif