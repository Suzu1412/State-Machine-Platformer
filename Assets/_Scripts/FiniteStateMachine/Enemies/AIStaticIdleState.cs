using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIStaticIdleState : State
{
    private Vector2 direction;

    private void OnValidate()
    {
        stateType = StateType.Idle;
    }

    internal override void EnterState()
    {
        fsm.Agent.AnimationManager.PlayAnimation(AnimationType.idle);
        fsm.Agent.Rb2d.velocity = Vector2.zero;
    }

    internal override void LogicUpdate()
    {
        HandleAttackTransition();
    }

    internal override void PhysicsUpdate()
    {
    }
}
