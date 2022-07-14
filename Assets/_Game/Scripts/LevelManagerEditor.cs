using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR
[CustomEditor(typeof(LevelManager))]
public class LevelEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        var script = (LevelManager)target;

        if (GUILayout.Button("Save Spawn Point"))
        {
            script.EditorSaveSpawnPoints();
        }
        if (GUILayout.Button("Clear Spawn Point"))
        {
            script.EditorClearSpawnPonts();
        }
        if (GUILayout.Button("Count Temp Brick"))
        {
            script.EditorCountCurrentSpawnPoints();
        }
    }
}
#endif