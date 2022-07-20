using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateDetect : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(ConstValues.TAG_PLAYER))
        {
            Player player = other.GetComponent<Player>();

            player.LevelStage++;
            LevelManager.Instance.SpawnBaseBrick(player.BrickTag,
            LevelManager.Instance.spawnPointCount[player.LevelStage] / ConstValues.VALUE_NUM_OF_PLAYER,
            LevelManager.Instance.spawnLocations[player.LevelStage]);
        }
    }
}
