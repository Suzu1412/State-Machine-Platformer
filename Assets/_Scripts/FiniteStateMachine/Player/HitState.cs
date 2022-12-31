using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitState : FallState
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

    public override void PhysicsUpdate()
    {
        fsm.Agent.Rb2d.velocity = new Vector2(0, fsm.Agent.Rb2d.velocity.y);
    }

    protected override void HandleMovement(Vector2 input)
    {
    }

    protected override void HandleFaceDirection(Vector2 input)
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
