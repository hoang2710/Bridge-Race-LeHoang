using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairRoot : MonoBehaviour
{
    public int NumOfStep;
    public Transform RootTrans;
    private void Start()
    {
        StartCoroutine(DelayMakeStair());
    }

    IEnumerator DelayMakeStair()
    {
        yield return new WaitForSeconds(0.5f);

        Vector3 pos = RootTrans.position;
        for (int i = 1; i < NumOfStep; i++)
        {
            pos += new Vector3(0, ConstValues.VALUE_STAIR_HEIGHT, ConstValues.VALUE_STAIR_WIDTH);
            PrefabManager.Instance.PopFromPool(ObjectType.InvisibleStair, pos, Quaternion.identity);
        }

        PrefabManager.Instance.PopFromPool(ObjectType.InvisibleStair, RootTrans.position, Quaternion.identity);

        RootTrans.gameObject.SetActive(false);
    }
}
