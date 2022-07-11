using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabManager : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public ObjectType tag;
        public GameObject objectPrefab;
        public int size;
    }
    public List<Pool> Pools;
    public Dictionary<ObjectType, Stack<GameObject>> poolDictionary = new Dictionary<ObjectType, Stack<GameObject>>();
    public static PrefabManager Instance { get; private set; }
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

    void Start()
    {
        foreach (Pool pool in Pools)
        {
            Stack<GameObject> objStack = new Stack<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.objectPrefab);
                obj.SetActive(false);
                objStack.Push(obj);
            }

            poolDictionary.Add(pool.tag, objStack);
        }
    }

    public GameObject PopFromPool(ObjectType tag, Vector3 spawnPos, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("pool not exist " + tag);
            return null;
        }

        GameObject obj = poolDictionary[tag].Peek();
        if (obj == null)
        {
            foreach (var item in Pools)
            {
                if (item.tag == tag)
                {
                    obj = Instantiate(item.objectPrefab);
                    break;
                }
            }
        }
        else
        {
            poolDictionary[tag].Pop();
        }

        Transform objTrans = obj.transform;

        obj.SetActive(true);
        objTrans.position = spawnPos;
        objTrans.rotation = rotation;

        IPooledObject pooledObject = obj.GetComponent<IPooledObject>();

        if (pooledObject != null)
        {
            pooledObject.OnObjectSpawn();
        }
        return obj;
    }
    public void PushToPool(ObjectType tag, GameObject obj)
    {
        poolDictionary[tag].Push(obj);
        obj.SetActive(false);
    }

    public enum ObjectType
    {
        BlueBrick,
        GreenBrick,
        RedBrick,
        YellowBrick
    }
}