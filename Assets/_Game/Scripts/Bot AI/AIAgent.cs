using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIAgent : MonoBehaviour
{
    public AIStateMachine stateMachine;
    public AIStateId initState;
    public NavMeshAgent NavAgent;
    public Transform BotTrans;
    public Animator anim;
    public Collider BotCollider;
    public LayerMask BrickLayerMask;
    public Stack<GameObject> BrickStatck;
    public Enemy enemyRef;
    public bool isOnEndOfStair;

    private void Start()
    {
        stateMachine = new AIStateMachine(this);
        stateMachine.RegisterState(new AICollectBrickState());
        stateMachine.RegisterState(new AIBuildBridgeState());
        stateMachine.RegisterState(new AIIdleState());
        stateMachine.RegisterState(new AIFallState());
        stateMachine.ChangeState(initState);

        BrickStatck = enemyRef.BrickStack;
    }
    private void Update()
    {
        stateMachine.Update();
    }
}
