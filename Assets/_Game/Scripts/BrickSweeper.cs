using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickSweeper : MonoBehaviour
{
    public Transform MainTrans;
    public float SweepRadius;

    public void SweepBrick(ObjectType tag, LayerMask brickLayerMask, Level_Stage playerLevelStage)
    {
        Collider[] cols = Physics.OverlapSphere(MainTrans.position, SweepRadius, brickLayerMask);

        int tmp = cols.Length;

        for (int i = 0; i < tmp; i++)
        {
            GameObject obj = cols[i].gameObject;
            Transform objTrans = obj.transform;

            //NOTE: Add spawn position back to spawnPoints List
            LevelManager.Instance.spawnLocations[playerLevelStage].Add(objTrans.position);

            PrefabManager.Instance.PushToPool(tag, obj);
        }
    }
}
