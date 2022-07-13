using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR
[CustomEditor(typeof(Spawner))]
public class SpawnerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        var script = (Spawner)target;

        if (GUILayout.Button("Save spawn position"))
        {
            script.GetSpawnPoint();
        }
        
    }
}
#endif