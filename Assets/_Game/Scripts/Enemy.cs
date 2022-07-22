using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Player
{
    public AIAgent agent;
    public NavMeshAgent NavAgent;
    public Transform targetTrans;
    private bool isFall;
    new private void Start()
    {
        StackRootLocalRotation = StackRootTrans.localRotation;
        Rb.constraints = rbMoveConstraints;
        NavAgent.updateRotation = false;
        NavAgent.autoBraking = false;
    }
    protected override void FixedUpdate()
    {
        //NOTE: Only get brick rotate Quaternion
        BrickRotation = PlayerTrans.rotation * StackRootLocalRotation;
    }
    // private void BotMove(Vector3 destination)
    // {
    //     if (!isFall)
    //     {
    //         if (NavAgent.velocity.sqrMagnitude < 0.01f)
    //         {
    //             anim.SetFloat(ConstValues.PLAYER_ANIM_VELOCITY, 0);
    //         }
    //         else
    //         {
    //             anim.SetFloat(ConstValues.PLAYER_ANIM_VELOCITY, 1f);
    //             MakeBotRotate();
    //         }
    //     }
    // }
    protected override void TriggerFall()
    {
        StartCoroutine(Fall());
    }
    private IEnumerator Fall()
    {
        agent.stateMachine.ChangeState(AIStateId.Fall);
        yield return new WaitForSeconds(ConstValues.VALUE_TIME_OF_FALL_ANIM); //NOTE: ~ time of fall plus kipup animation
        agent.stateMachine.ChangeState(agent.stateMachine.prevState);
    }
}
