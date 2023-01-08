using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitState : FallState
{
    private float hitStunDuration;
    private bool hitForcesApplied;

    protected override void EnterState()
    {
        hitStunDuration = fsm.Agent.Data.HitStunDuration;
        fsm.Agent.AnimationManager.PlayAnimation(AnimationType.hit);
        fsm.Agent.Rb2d.velocity = new Vector2(0, fsm.Agent.Rb2d.velocity.y);
        hitForcesApplied = false;
    }

    public override void LogicUpdate()
    {
        hitStunDuration -= Time.deltaTime;

        if (hitStunDuration < 0f)
        {
            fsm.TransitionToState(StateType.Idle);
        }
    }

    public override void PhysicsUpdate()
    {
        fsm.Agent.CollissionSenses.DetectGround();
        fsm.Agent.CollissionSenses.DetectWall();
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

    protected override void HitKnockback()
    {
        if (hitForcesApplied) return;

        Vector2 theForce = new Vector2(fsm.Agent.KnockbackSystem.KnockbackDirection * fsm.Agent.Data.KnockbackForce.x, fsm.Agent.Data.KnockbackForce.y);

        fsm.Agent.Rb2d.AddForce(theForce, ForceMode2D.Impulse);

        hitForcesApplied = true;
    }

}
