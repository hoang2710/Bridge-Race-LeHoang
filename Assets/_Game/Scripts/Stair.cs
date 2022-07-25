using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stair : MonoBehaviour, IPooledObject
{
    public Transform StairTrans;
    public GameObject StairObject;
    public ObjectType StairTag;
    public bool isEndOfStair;

    public void OnObjectSpawn()
    {
        isEndOfStair = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(ConstValues.TAG_PLAYER))
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                if (player.MinusBrick())
                {
                    SwitchBrick(player.StairTag);

                    if (isEndOfStair)
                    {
                        AIAgent agent = player.GetComponent<AIAgent>();

                        if (agent != null)
                        {
                            agent.isOnEndOfStair = true;
                        }
                    }
                }
            }
        }
    }
    private void SwitchBrick(ObjectType targetTag)
    {
        //NOTE: Pass isEndOfStair value to switched stair
        GameObject obj = PrefabManager.Instance.PopFromPool(targetTag, StairTrans.position, Quaternion.identity);
        
        if (isEndOfStair && obj != null)
        {
            Stair stair = obj.GetComponent<Stair>();

            if (stair != null)
            {
                stair.isEndOfStair = true;
            }
        }

        PrefabManager.Instance.PushToPool(StairTag, StairObject);
    }
}
