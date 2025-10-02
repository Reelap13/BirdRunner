#if UNITY_EDITOR
using Game.Level.Constructor.Tube;
using UnityEditor;
using UnityEngine;

namespace Game.Level.Constructor.TubeRings
{
    [CustomEditor(typeof(TubeRingsContructor))]
    public class TubeRingsContructorEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            TubeRingsContructor gen = (TubeRingsContructor)target;
            if (GUILayout.Button("Generate Tube"))
            {
                gen.GenerateRings();
            }
        }
    }
}
#endif