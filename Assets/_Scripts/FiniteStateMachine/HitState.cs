using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitState : State
{
    protected override void EnterState()
    {
        fsm.Agent.AnimationManager.PlayAnimation(AnimationType.hit);
    }

    public override void LogicUpdate()
    {
        if (!fsm.Agent.HealthSystem.isInvulnerable)
        {
            fsm.TransitionToState(StateType.Idle);
        }
    }

    protected override void ExitState()
    {
    }


    protected override void HandleMovement(Vector2 input)
    {
    }

    protected override void HandleJumpPressed()
    {
    }

    protected override void HandleJumpReleased()
    {
    }

    protected override void HandleAttackPressed()
    {
    }

    protected override void HandleRollPressed()
    {
    }

    protected override void HandleRollReleased()
    {
    }
}
