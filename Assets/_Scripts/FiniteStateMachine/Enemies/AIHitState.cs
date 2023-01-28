using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIHitState : State
{
    private float hitStunDuration;

    internal override void EnterState()
    {
        hitStunDuration = fsm.Agent.Data.HitStunDuration;
        fsm.Agent.AnimationManager.PlayAnimation(AnimationType.hit);
        fsm.Agent.Rb2d.velocity = new Vector2(0, fsm.Agent.Rb2d.velocity.y);
    }

    internal override void LogicUpdate()
    {
        hitStunDuration -= Time.deltaTime;

        if (hitStunDuration < 0f)
        {
            fsm.TransitionToState(fsm.PreviousStateType);
        }
    }

    internal override void PhysicsUpdate()
    {
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

    protected override void HitKnockback()
    {
        Vector2 theForce = new Vector2(fsm.Agent.KnockbackSystem.KnockbackDirection * fsm.Agent.Data.KnockbackForce.x, fsm.Agent.Data.KnockbackForce.y);

        fsm.Agent.Rb2d.AddForce(theForce, ForceMode2D.Impulse);
    }
}
