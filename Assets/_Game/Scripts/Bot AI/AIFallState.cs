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
        agent.BotCollider.enabled = false;
        agent.NavAgent.enabled = false;
        agent.anim.SetTrigger(ConstValues.PLAYER_ANIM_FALL);
    }
    public void Update(AIAgent agent)
    {

    }
    public void Exit(AIAgent agent)
    {
        agent.BotCollider.enabled = true;
        agent.NavAgent.enabled = true;
    }
}
