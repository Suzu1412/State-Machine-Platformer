using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIFallState : State
{
    private void OnValidate()
    {
        stateType = StateType.Fall;
    }

    internal override void LogicUpdate()
    {
        if (fsm.Agent.CollissionSenses.IsGrounded)
        {
            fsm.TransitionToState(StateType.Idle);
        }
    }
}
