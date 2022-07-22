using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stair : MonoBehaviour, IPooledObject
{
    public Transform StairTrans;
    public GameObject StairObject;
    public PrefabManager.ObjectType StairTag;

    public void OnObjectSpawn()
    {

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
                }
            }
        }
    }
    private void SwitchBrick(PrefabManager.ObjectType targetTag)
    {
        PrefabManager.Instance.PopFromPool(targetTag, StairTrans.position, Quaternion.identity);
        PrefabManager.Instance.PushToPool(StairTag, StairObject);
    }
}
