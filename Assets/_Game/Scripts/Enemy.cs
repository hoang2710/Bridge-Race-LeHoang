using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Player
{
    public NavMeshAgent NavAgent;
    public Transform targetTrans;
    private void Update()
    {
        NavAgent.destination = targetTrans.position;
    }
    protected override void FixedUpdate()
    {
        //NOTE: .........
    }
    public override void TriggerFall()
    {
        StartCoroutine(Fall());
    }

    private IEnumerator Fall()
    {
        anim.SetTrigger(ConstValues.PLAYER_ANIM_FALL);
        yield return new WaitForSeconds(5.3f); //NOTE: ~ time of fall plus kipup animation
        Debug.Log("Stop moving");
    }
}
