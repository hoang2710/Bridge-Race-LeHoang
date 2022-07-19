using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class LevelManager : MonoBehaviour
{
    public List<Vector3> StairRoot;
    public Level_Stage curLevelStage;
    public Dictionary<Level_Stage, List<Vector3>> spawnLocations = new Dictionary<Level_Stage, List<Vector3>>();
    // private Dictionary<Level_Stage, Queue<Vector3>> tempSpawnLocation = new Dictionary<Level_Stage, Queue<Vector3>>();
    #region Singleton
    public static LevelManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion
    private void Start()
    {
        GameManager.OnGameStateChanged += GameManagerOnGameStateChanged;
    }
    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= GameManagerOnGameStateChanged;
    }
    private void GameManagerOnGameStateChanged(GameManager.GameState state)
    {
        switch (state)
        {
            case GameManager.GameState.LoadLevel:
                LoadLevel();
                break;
            case GameManager.GameState.Loading:
                LoadGame();
                break;
            default:
                break;
        }
    }
    public void LoadGame()
    {
        Debug.Log("Load Game");
        foreach (var item in System.Enum.GetValues(typeof(Level_Stage)))
        {
            var levelData = Resources.Load<LevelData>($"Level Data/{item.ToString()}");
            if (levelData != null)
            {
                spawnLocations.Add(levelData.Level_Stage, levelData.SpawnPoints);
                Debug.Log(levelData.Level_Stage.ToString() + "   " + levelData.SpawnPoints.Count + "    " + spawnLocations.Count);
            }
        }
    }
    public void LoadLevel()
    {
        Debug.Log("Load Level");
        List<Vector3> spawnPoints = spawnLocations[curLevelStage];
        //NOTE: temp code solution for debug
        #region TEMP CODE FOR SPAWN BASE BRICK
        int num = spawnPoints.Count / 4;
        int tempNum = num;
        while (tempNum-- > 0)
        {
            SpawnObject(spawnPoints, PrefabManager.ObjectType.BlueBrick);
        }
        tempNum = num;
        while (tempNum-- > 0)
        {
            SpawnObject(spawnPoints, PrefabManager.ObjectType.GreenBrick);
        }
        tempNum = num;
        while (tempNum-- > 0)
        {
            SpawnObject(spawnPoints, PrefabManager.ObjectType.RedBrick);
        }
        tempNum = num;
        while (tempNum-- > 0)
        {
            SpawnObject(spawnPoints, PrefabManager.ObjectType.YellowBrick);
        }
        //NOTE: temp solution fo make invisible stair
        MakeStair();
        #endregion
    }
    public void SpawnObject(List<Vector3> spawnPoints, PrefabManager.ObjectType objectType)
    {
        // Debug.Log(spawnPoints.Count);
        if (spawnPoints.Count <= 0)
        {
            return;
        }
        int ran = Random.Range(0, spawnPoints.Count);
        PrefabManager.Instance.PopFromPool(objectType, spawnPoints[ran], Quaternion.identity);
        spawnPoints.RemoveAt(ran);
    }
    private void MakeStair()
    {
        float stairHeight = 0.15f;
        float stairWidth = 0.18f;


        foreach (var item in StairRoot)
        {
            Vector3 pos = item;
            int numOfStep = 15;
            while (numOfStep > 0)
            {
                pos += new Vector3(0, stairHeight, stairWidth);
                PrefabManager.Instance.PopFromPool(PrefabManager.ObjectType.InvisibleStair, pos, Quaternion.identity);
                numOfStep--;
            }
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

        foreach (var item in gameObjects)
        {
            item.SetActive(true);
        }
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
