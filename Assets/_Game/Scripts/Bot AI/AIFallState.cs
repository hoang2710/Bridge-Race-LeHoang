using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIFallState : AIState
{
    public AIStateId GetId()
    {
        return AIStateId.Fall;
    }
    public void Enter(AIAgent agent)
    {
        agent.NavAgent.destination = agent.BotTrans.position;
        agent.BotCollider.enabled = false;
        agent.anim.SetTrigger(ConstValues.PLAYER_ANIM_FALL);
    }
    public void Update(AIAgent agent)
    {

    }
    public void Exit(AIAgent agent)
    {
        agent.BotCollider.enabled = true;
    }
}
