#if UNITY_EDITOR
using Game.Level.Constructor.Tube;
using UnityEditor;
using UnityEngine;


namespace Game.Level.Constructor.Obstacles
{
    [CustomEditor(typeof(ObstaclesConstructor))]
    public class ObstaclesConstructorEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            ObstaclesConstructor gen = (ObstaclesConstructor)target;
            if (GUILayout.Button("Generate Obstacles"))
            {
                gen.Generate(true);
            }
            if (GUILayout.Button("Destroy Obstacles"))
            {
                gen.DestroySpawnedObjects(true);
            }
            if (GUILayout.Button("Update From Scene"))
            {
                gen.UpdateObstaclesFromChildren();
            }

        }
    }
}
#endif