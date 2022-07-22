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

        List<Vector3> waypoints = WaypointRefCenter.Instance.waypoints;
        int ran = Random.Range(0, waypoints.Count);
        agent.NavAgent.destination = waypoints[ran]; Debug.LogWarning("settle");
    }
    public void Update(AIAgent agent)
    {
        MakeBotRotate(agent);

        if (agent.BrickStatck.Count <= 0) 
        {
            agent.stateMachine.ChangeState(AIStateId.CollectBrick);
        }
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
