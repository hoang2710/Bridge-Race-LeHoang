using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIIdleState : AIState
{
    public AIStateId GetId()
    {
        return AIStateId.Idle;
    }
    public void Enter(AIAgent agent)
    {
        agent.NavAgent.destination = agent.BotTrans.position;
        agent.anim.SetFloat(ConstValues.PLAYER_ANIM_VELOCITY, 0); 
    }
    public void Update(AIAgent agent)
    {

    }
    public void Exit(AIAgent agent)
    {

    }
}
