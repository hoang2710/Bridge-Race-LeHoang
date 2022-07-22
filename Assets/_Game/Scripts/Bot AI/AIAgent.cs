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

    private void Start() {
        stateMachine = new AIStateMachine(this);
        stateMachine.RegisterState(new AICollectBrickState());
        stateMachine.RegisterState(new AIBuildBridgeState());
        stateMachine.RegisterState(new AIIdleState());
        stateMachine.RegisterState(new AIFallState());
        stateMachine.ChangeState(initState);
    }
    private void Update() {
        stateMachine.Update();
    }
}
