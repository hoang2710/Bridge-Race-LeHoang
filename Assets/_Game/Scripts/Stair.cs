using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stair : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(ConstValues.TAG_PLAYER))
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.MinusBrick();
                Debug.Log(gameObject.name);
            }
        }
    }
}
