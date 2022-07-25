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
        Rb.constraints = ConstValues.RB_DEFAULT_CONSTRAINTS;
        NavAgent.updateRotation = false;
        NavAgent.autoBraking = false;

        GameManager.OnGameStateChanged += GameManagerOnGameStateChanged; //NOTE: there is no other way
    }
    protected override void FixedUpdate()
    {
        //NOTE: Only get brick rotate Quaternion
        BrickRotation = PlayerTrans.rotation * StackRootLocalRotation;
    }
    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= GameManagerOnGameStateChanged;
    }
    private void GameManagerOnGameStateChanged(GameManager.GameState state)
    {
        switch (state)
        {
            case GameManager.GameState.Playing:
                agent.stateMachine.ChangeState(AIStateId.CollectBrick);
                break;
            default:
                break;
        }
    }
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
