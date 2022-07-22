using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICollectBrickState : AIState
{
    private Vector3 targetPos;
    private bool isBuild;
    private bool isNeedToFindBrick;
    public AIStateId GetId()
    {
        return AIStateId.CollectBrick;
    }
    public void Enter(AIAgent agent)
    {
        agent.anim.SetFloat(ConstValues.PLAYER_ANIM_VELOCITY, 1f);
        isNeedToFindBrick = true;
        isBuild = false;
    }
    public void Update(AIAgent agent)
    {
        MakeBotRotate(agent);

        Vector3 moveDir = targetPos - agent.BotTrans.position;
        if (moveDir.sqrMagnitude < ConstValues.VALUE_BOT_MIN_TOUCH_BRICK_DISTANCE || isNeedToFindBrick)
        // if(agent.NavAgent.velocity.sqrMagnitude < 0.01f)
        {
            FindBrick(agent);
            agent.NavAgent.destination = targetPos; Debug.LogWarning("assign");
        }

        Debug.Log("TargetPos  " + agent.gameObject.name + "   " + targetPos + "   " + agent.BotTrans.position);
        // else
        // {
        //     agent.NavAgent.velocity = moveDir.normalized * agent.moveSpeed;
        // }
        if (isBuild)
        {
            agent.stateMachine.ChangeState(AIStateId.BuildBridge);
        }
    }
    public void Exit(AIAgent agent)
    {

    }
    private void MakeBotRotate(AIAgent agent)
    {
        Vector3 botVelocity = agent.NavAgent.velocity;

        if (botVelocity.sqrMagnitude > 0.01f)
        {
            Vector3 dir = new Vector3(botVelocity.x, 0, botVelocity.z).normalized;
            agent.BotTrans.rotation = Quaternion.LookRotation(dir);
        }
    }
    private void FindBrick(AIAgent agent)
    {
        Collider[] cols = Physics.OverlapSphere(agent.BotTrans.position, ConstValues.VALUE_BOT_DETECT_RANGE, agent.BrickLayerMask);
        Debug.Log(cols.Length + "  " + agent.gameObject.name);
        if (cols.Length > 0)
        {
            int ran = Random.Range(0, cols.Length);
            targetPos = cols[ran].transform.position; Debug.Log(ran + "  " + agent.gameObject.name + "  " + targetPos);

            isNeedToFindBrick = false;
        }
        else
        {
            if (agent.BrickStatck.Count > 1) //NOTE: temp solution for buildBridgeState condition !!!
            {
                isBuild = true;
            }
        }
    }
}
