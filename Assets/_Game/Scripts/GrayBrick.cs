using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrayBrick : Brick
{
    protected override void AddBrickToPlayer(Player player)
    {
        GameObject tmpBrickObj = PrefabManager.Instance.PopFromPool(player.BrickTag, BrickTrans.position, BrickTrans.rotation);
        Transform tmpBrickTrans = tmpBrickObj.transform;

        tmpBrickTrans.SetParent(player.PlayerTrans);
        tmpBrickTrans.position = player.StackRootTrans.position;
        player.StackRootTrans.position += player.StackRootTrans.up * brickHeight;
        tmpBrickTrans.rotation = player.BrickRotation;

        player.BrickStack.Push(BrickObj);

        PrefabManager.Instance.PushToPool(PrefabManager.ObjectType.GrayBrick, BrickObj);
    }
}
