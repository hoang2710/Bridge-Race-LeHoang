using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateDetect : MonoBehaviour
{
    public Level_Stage targetLevelStage;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(ConstValues.TAG_PLAYER))
        {
            Player player = other.GetComponent<Player>();

            if (player != null)
            {
                if ((int)player.LevelStage != (int)targetLevelStage)
                {
                    //NOTE: update current Level_Stage status to target stage
                    player.LevelStage = targetLevelStage;
                    LevelManager.Instance.SpawnBaseBrick(player.BrickTag,
                    LevelManager.Instance.spawnPointCount[player.LevelStage] / ConstValues.VALUE_NUM_OF_PLAYER,
                    LevelManager.Instance.spawnLocations[player.LevelStage]);
                }
            }
        }
    }
}
