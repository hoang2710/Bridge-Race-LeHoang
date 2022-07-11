using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public Transform BrickTrans;
    [SerializeField]
    private float brickHeight = 0.03f;
    public GameObject BrickObj;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(ConstValues.PLAYER_TAG))
        {
            Player player = other.GetComponent<Player>();

            if (player != null)
            {
                //NOTE: Add brick to player stack 
                BrickTrans.parent = player.PlayerTrans;
                BrickTrans.position = player.StackRootTrans.position;
                player.StackRootTrans.position += player.StackRootTrans.up * brickHeight;
                BrickTrans.rotation = player.Rotation;

                player.BrickStack.Push(BrickObj);
            }
        }
    }

}
