using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Player
{
    public NavMeshAgent NavAgent;
    public Transform targetTrans;
    private bool isFall;
    private void Update()
    {
        if (!isFall)
        {
            NavAgent.destination = targetTrans.position;

            if (NavAgent.velocity.sqrMagnitude < 0.01f)
            {
                anim.SetFloat(ConstValues.PLAYER_ANIM_VELOCITY, 0);
            }
            else
            {
                anim.SetFloat(ConstValues.PLAYER_ANIM_VELOCITY, 1f);
            }
        }

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
        isFall = true;
        NavAgent.destination = PlayerTrans.position;
        anim.SetTrigger(ConstValues.PLAYER_ANIM_FALL);
        yield return new WaitForSeconds(5.3f); //NOTE: ~ time of fall plus kipup animation
        isFall = false;
    }
}
