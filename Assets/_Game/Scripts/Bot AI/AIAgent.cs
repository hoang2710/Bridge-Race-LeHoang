using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAgent : MonoBehaviour
{
    public AIStateMachine stateMachine;
    public AIStateId initState;

    private void Start() {
        stateMachine = new AIStateMachine(this);
        stateMachine.ChangeState(initState);
    }
    private void Update() {
        stateMachine.Update();
    }
}
