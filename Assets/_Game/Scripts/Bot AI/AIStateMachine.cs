using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIStateMachine
{
    public AIState[] states;
    public AIAgent agent;
    public AIStateId currentState;
    public AIStateId prevState;

    public AIStateMachine(AIAgent agent)
    {
        this.agent = agent;
        int numOfStates = System.Enum.GetNames(typeof(AIStateId)).Length;
        states = new AIState[numOfStates];
    }
    public void RegisterState(AIState state)
    {
        int i = (int)state.GetId();
        states[i] = state;
    }
    public AIState GetState(AIStateId stateId)
    {
        int i = (int)stateId;
        return states[i];
    }
    public void Update()
    {
        GetState(currentState)?.Update(agent);
    }
    public void ChangeState(AIStateId newState)
    {
        GetState(currentState)?.Exit(agent);
        prevState = currentState;
        currentState = newState;
        GetState(currentState)?.Enter(agent);
    }
}
