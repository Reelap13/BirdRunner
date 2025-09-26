#if UNITY_EDITOR
using Game.Level.Constructor.Line;
using UnityEditor;
using UnityEngine;

namespace Game.Level.Constructor.Line
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
                gen.GenerateLine();
            }
        }
    }
}
#endif