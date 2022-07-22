using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrayBrick : Brick
{
    public Collider Col;

    public override void OnObjectSpawn()
    {
        Col.enabled = false;
        StartCoroutine(DelaySetCanPickStatus());
    }
    protected override void AddBrickToPlayer(Player player)
    {
        GameObject tmpBrickObj = PrefabManager.Instance.PopFromPool(player.BrickTag, BrickTrans.position, BrickTrans.rotation);
        Transform tmpBrickTrans = tmpBrickObj.transform;

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
