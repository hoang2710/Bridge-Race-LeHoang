using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Player
{
    public NavMeshAgent NavAgent;
    public Transform targetTrans;
    private bool isFall;
    new private void Start()
    {
        StackRootLocalRotation = StackRootTrans.localRotation;
        Rb.constraints = rbMoveConstraints;
        NavAgent.updateRotation = false;
    }
    private void Update()
    {
        BotMove();
    }
    protected override void FixedUpdate()
    {
        // NOTE: Only get brick rotate Quaternion
        BrickRotation = PlayerTrans.rotation * StackRootLocalRotation;
    }
    private void BotMove()
    {
        if (isFall)
        {
            NavAgent.destination = PlayerTrans.position;
        }
        else
        {
            NavAgent.destination = targetTrans.position;

            if (NavAgent.velocity.sqrMagnitude < 0.01f)
            {
                anim.SetFloat(ConstValues.PLAYER_ANIM_VELOCITY, 0);
            }
            else
            {
                anim.SetFloat(ConstValues.PLAYER_ANIM_VELOCITY, 1f);
                MakeBotRotate();
            }
        }
    }
    private void MakeBotRotate()
    {
        Vector3 dir = new Vector3(NavAgent.velocity.x, 0, NavAgent.velocity.z).normalized;
        PlayerTrans.rotation = Quaternion.LookRotation(dir);
    }
    protected override void TriggerFall()
    {
        StartCoroutine(Fall());
    }

    private IEnumerator Fall()
    {
        isFall = true;
        Col.enabled = false;
        anim.SetTrigger(ConstValues.PLAYER_ANIM_FALL);
        yield return new WaitForSeconds(ConstValues.VALUE_TIME_OF_FALL_ANIM); //NOTE: ~ time of fall plus kipup animation
        isFall = false;
        Col.enabled = true;
    }
}
