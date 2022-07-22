using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrayBrick : MonoBehaviour, IPooledObject
{
    public Transform BrickTrans;
    [SerializeField]
    public static float brickHeight = 0.03f;
    public GameObject BrickObj;
    [SerializeField]
    public Collider Col;

    public void OnObjectSpawn()
    {
        Col.enabled = false;
        StartCoroutine(DelaySetCanPickStatus());
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(ConstValues.TAG_PLAYER))
        {
            Player player = other.GetComponent<Player>();

            if (player != null)
            {
                AddTrueBrickToPlayer(player);
            }
        }
    }
    private void AddTrueBrickToPlayer(Player player)
    {
        GameObject tmpBrickObj = PrefabManager.Instance.PopFromPool(player.BrickTag, BrickTrans.position, BrickTrans.rotation);
        Transform tmpBrickTrans = tmpBrickObj.transform;
        Brick tmpBrick = tmpBrickObj.GetComponent<Brick>();

        tmpBrick.Col.enabled = false;

        tmpBrickTrans.SetParent(player.PlayerTrans);
        tmpBrickTrans.position = player.StackRootTrans.position;
        player.StackRootTrans.position += player.StackRootTrans.up * brickHeight;
        tmpBrickTrans.rotation = player.BrickRotation;

        player.BrickStack.Push(tmpBrickObj);

        PrefabManager.Instance.PushToPool(PrefabManager.ObjectType.GrayBrick, BrickObj);
    }

    private IEnumerator DelaySetCanPickStatus()
    {
        yield return new WaitForSeconds(2f);
        Col.enabled = true;
    }
}
