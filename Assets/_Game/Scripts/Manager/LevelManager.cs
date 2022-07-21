using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class LevelManager : Singleton<LevelManager>
{
    public struct PlayerRef{
        public GameObject obj;
        public Transform objTrans;
        public Level_Stage LevelStage; 
    }
    public List<PlayerRef> PlayerList;
    public Level_Stage curLevelStage;
    public List<LevelData> LevelDataScriptsList;
    public Dictionary<Level_Stage, List<Vector3>> spawnLocations = new Dictionary<Level_Stage, List<Vector3>>();
    public Dictionary<Level_Stage, int> spawnPointCount = new Dictionary<Level_Stage, int>();
    public void LoadGame()
    {
        Debug.Log("Load Game");
        foreach (var item in LevelDataScriptsList)
        {
            List<Vector3> points = new List<Vector3>(item.SpawnPoints);
            spawnLocations.Add(item.Level_Stage, points);
            spawnPointCount.Add(item.Level_Stage, points.Count);
            Debug.Log(item.Level_Stage.ToString() + "   " + points.Count + "    " + spawnLocations.Count);
        }
    }
    public void LoadLevel()
    {
        Debug.Log("Load Level");
        List<Vector3> spawnPoints = spawnLocations[curLevelStage];

        int num = spawnPointCount[curLevelStage] / ConstValues.VALUE_NUM_OF_PLAYER;
        SpawnBaseBrick(PrefabManager.ObjectType.BlueBrick, num, spawnPoints);
        SpawnBaseBrick(PrefabManager.ObjectType.GreenBrick, num, spawnPoints);
        SpawnBaseBrick(PrefabManager.ObjectType.RedBrick, num, spawnPoints);
        SpawnBaseBrick(PrefabManager.ObjectType.YellowBrick, num, spawnPoints);
    }
    public void LoadNextLevel()
    {
        curLevelStage = (Level_Stage)(((int)curLevelStage + 3) % 9);
        LoadLevel();
    }
    public void SpawnObject(List<Vector3> spawnPoints, PrefabManager.ObjectType objectType)
    {
        // Debug.Log(spawnPoints.Count + "   " + spawnLocations[curLevelStage].Count);
        if (spawnPoints.Count <= 0)
        {
            return;
        }
        int ran = Random.Range(0, spawnPoints.Count);
        PrefabManager.Instance.PopFromPool(objectType, spawnPoints[ran], Quaternion.identity);
        spawnPoints.RemoveAt(ran);
    }
    public void SpawnBaseBrick(PrefabManager.ObjectType tag, int num, List<Vector3> spawnPoints)
    {
        for (int i = 0; i < num; i++)
        {
            SpawnObject(spawnPoints, tag);
        }
    }

    public enum Level_Stage
    {
        Level_1_Stage_1,
        Level_1_Stage_2,
        Level_1_Stage_3,
        Level_2_Stage_1,
        Level_2_Stage_2,
        Level_2_Stage_3,
        Level_3_Stage_1,
        Level_3_Stage_2,
        Level_3_Stage_3
    }

#if UNITY_EDITOR
    public GameObject CurSpawnLocation;
    public void EditorSaveSpawnPoints()
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag(ConstValues.TAG_SPAWN_LOCATION);
        foreach (var item in gameObjects)
        {
            item.SetActive(false);
        }

        CurSpawnLocation.SetActive(true);

        List<Vector3> spawnPoints = GetSpawnPoint();

        var levelData = ScriptableObject.CreateInstance<LevelData>();
        levelData.SpawnPoints = spawnPoints;
        levelData.Level_Stage = curLevelStage;

        SaveToAsset(levelData);

        CurSpawnLocation.SetActive(false);
    }
    public void EditorClearSpawnPonts()
    {
        AssetDatabase.DeleteAsset($"Assets/_Game/Resources/Level Data/{curLevelStage.ToString()}.asset");
    }
    public void EditorCountCurrentSpawnPoints()
    {
        Debug.Log(CurSpawnLocation.transform.childCount);
    }
    public List<Vector3> GetSpawnPoint()
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag(ConstValues.TAG_BRICK_SPAWN_POINT);
        List<Vector3> spawnPoints = new List<Vector3>();
        foreach (var item in gameObjects)
        {
            spawnPoints.Add(item.transform.position);
        }

        return spawnPoints;
    }
    private void SaveToAsset(LevelData level)
    {
        var levelData = Resources.Load<LevelData>($"Level Data/{curLevelStage.ToString()}");
        if (levelData != null)
        {
            Debug.LogWarning("Data for current Level_Stage exist !!!");
            return;
        }

        AssetDatabase.CreateAsset(level, $"Assets/_Game/Resources/Level Data/{curLevelStage.ToString()}.asset");
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
#endif
}
