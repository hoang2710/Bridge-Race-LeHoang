using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelData : ScriptableObject
{
    public Level_Stage Level_Stage;
    public List<Vector3> SpawnPoints;
}
