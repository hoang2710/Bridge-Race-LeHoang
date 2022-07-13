using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private LevelManager.Level level;
    [SerializeField]
    private LevelManager.Stage stage;
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

    }
    public void SpawnObject(List<Vector3> spawnPoints)
    {
        int ran = Random.Range(0, spawnPoints.Count);
        PrefabManager.Instance.PopFromPool(PrefabManager.ObjectType.BlueBrick, spawnPoints[ran], Quaternion.identity);
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

    public enum Stage
    {
        Stage_1,
        Stage_2,
        Stage_3
    }
    public enum Level
    {
        Level_1,
        Level_2,
        Level_3
    }
}
