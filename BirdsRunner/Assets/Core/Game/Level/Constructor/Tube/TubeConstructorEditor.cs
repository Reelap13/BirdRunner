#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Game.Level.Constructor.Tube
{
    [CustomEditor(typeof(TubeConstructor))]
    public class TubeConstructorEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            TubeConstructor gen = (TubeConstructor)target;
            if (GUILayout.Button("Generate Tube"))
            {
                gen.Generate();
            }
        }
    }
}
#endif