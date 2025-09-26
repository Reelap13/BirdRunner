#if UNITY_EDITOR
using Game.Level.Constructor.Tube;
using UnityEditor;
using UnityEngine;

namespace Game.Level.Constructor
{
    [CustomEditor(typeof(LevelConstructor))]
    public class LevelConstructorEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            LevelConstructor gen = (LevelConstructor)target;
            if (GUILayout.Button("Generate Level"))
            {
                gen.GenerateLevel();
            }
        }
    }
}
#endif