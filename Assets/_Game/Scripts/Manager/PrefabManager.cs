using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabManager : Singleton<PrefabManager>
{
    [System.Serializable]
    public class Pool
    {
        public ObjectType tag;
        public GameObject objectPrefab;
        public int size;
    }
    [NonReorderable] //NOTE: display first element of list like shit --> use Nonreoderable to fix
    public List<Pool> Pools;
    public Dictionary<ObjectType, Stack<GameObject>> poolDictionary = new Dictionary<ObjectType, Stack<GameObject>>();
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
        GameObject obj = CheckIfHaveItemLeftInPool(tag);

        if (obj == null)
        {
            return obj;
        }

        Transform objTrans = obj.transform;

        obj.SetActive(true);
        objTrans.position = spawnPos;
        objTrans.rotation = rotation;

        IPooledObject pooledObject = obj.GetComponent<IPooledObject>(); //NOTE:currently finding solution for cache component

        if (pooledObject != null)
        {
            pooledObject.OnObjectSpawn();
        }
        return obj;
    }
    private GameObject CheckIfHaveItemLeftInPool(ObjectType tag)
    {
        if (poolDictionary[tag].Count > 0)
        {
            GameObject obj = poolDictionary[tag].Peek();
            poolDictionary[tag].Pop();
            return obj;
        }
        else
        {
            foreach (var item in Pools)
            {
                if (item.tag == tag)
                {
                    GameObject obj = Instantiate(item.objectPrefab);
                    return obj;
                }
            }
        }
        return null;
    }
    public void PushToPool(ObjectType tag, GameObject obj)
    {
        obj.transform.SetParent(null);
        poolDictionary[tag].Push(obj);
        obj.SetActive(false);
    }


}
public enum ObjectType
{
    BlueBrick,
    GreenBrick,
    RedBrick,
    YellowBrick,
    GrayBrick,
    BlueStair,
    GreenStair,
    RedStair,
    YellowStair,
    InvisibleStair
}