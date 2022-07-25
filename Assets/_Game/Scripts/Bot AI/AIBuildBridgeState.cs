using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBuildBridgeState : AIState
{
    private float timer;
    public AIStateId GetId()
    {
        return AIStateId.BuildBridge;
    }
    public void Enter(AIAgent agent)
    {
        agent.anim.SetFloat(ConstValues.PLAYER_ANIM_VELOCITY, 1f);

        List<Transform> waypoints = WaypointRefCenter.Instance.WaypointsRef[agent.enemyRef.LevelStage];
        int ran = Random.Range(0, waypoints.Count);
        agent.NavAgent.destination = waypoints[ran].position;
        // Debug.LogWarning("settle");

        timer = 0f;
    }
    public void Update(AIAgent agent)
    {
        MakeBotRotate(agent);

        if (agent.isOnEndOfStair)
        {
            if (timer > ConstValues.VALUE_TIME_FOR_BOT_GO_INTO_NEW_STAGE)
            {
                agent.stateMachine.ChangeState(AIStateId.CollectBrick); Debug.LogWarning("Chang State Due to last stair  " + agent.gameObject.name);
            }
            else
            {
                timer += Time.deltaTime;
            }
        }
        else if (agent.BrickStatck.Count <= 0)
        {
            agent.stateMachine.ChangeState(AIStateId.CollectBrick);
        }
    }
    public void Exit(AIAgent agent)
    {
        agent.isOnEndOfStair = false;
    }
    private void MakeBotRotate(AIAgent agent)
    {
        Vector3 dir = new Vector3(agent.NavAgent.velocity.x, 0, agent.NavAgent.velocity.z).normalized;
        agent.BotTrans.rotation = Quaternion.LookRotation(dir);
    }
}
