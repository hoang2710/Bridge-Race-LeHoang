using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBuildBridgeState : AIState
{
    public AIStateId GetId()
    {
        return AIStateId.BuildBridge;
    }
    public void Enter(AIAgent agent)
    {
        agent.anim.SetFloat(ConstValues.PLAYER_ANIM_VELOCITY, 1f);
    }
    public void Update(AIAgent agent)
    {
        // MakeBotRotate(agent);
    }
    public void Exit(AIAgent agent)
    {

    }
    private void MakeBotRotate(AIAgent agent)
    {
        Vector3 dir = new Vector3(agent.NavAgent.velocity.x, 0, agent.NavAgent.velocity.z).normalized;
        agent.BotTrans.rotation = Quaternion.LookRotation(dir);
    }
}
