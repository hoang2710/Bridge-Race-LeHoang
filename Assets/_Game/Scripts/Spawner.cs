using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // [NonReorderable] //NOTE: display first element of list like shit --> use Nonreoderable to fix
    // public List<GameObject> SpawnPointObjects;
    // [SerializeField]
    // private List<Vector3> spawnPoints = new List<Vector3>();
    [SerializeField]
    private LevelManager.Level level;
    [SerializeField]
    private LevelManager.Stage stage;

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
}
