using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour, IPooledObject
{
    public Transform BrickTrans;
    [SerializeField]
    public static float brickHeight = 0.03f;
    public GameObject BrickObj;

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
                AddBrickToPlayer(player);
            }
        }
    }
    private void AddBrickToPlayer(Player player)
    {
        //NOTE: Add spawn position back to spawnPoints List
        LevelManager.Instance.spawnLocations[LevelManager.Instance.curLevelStage].Add(BrickTrans.position);

        BrickTrans.SetParent(player.PlayerTrans);
        BrickTrans.position = player.StackRootTrans.position;
        player.StackRootTrans.position += player.StackRootTrans.up * brickHeight;
        BrickTrans.rotation = player.BrickRotation;

        player.BrickStack.Push(BrickObj);
    }

}
