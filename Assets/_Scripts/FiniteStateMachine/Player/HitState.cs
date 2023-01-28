using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitState : FallState
{
    private bool hitForcesApplied;

    private void OnValidate()
    {
        stateType = StateType.Hit;
    }

    internal override void EnterState()
    {
        fsm.Agent.AnimationManager.PlayAnimation(AnimationType.hit);
        fsm.Agent.Rb2d.velocity = new Vector2(0, fsm.Agent.Rb2d.velocity.y);
        hitForcesApplied = false;
    }

    internal override void LogicUpdate()
    {
    }

    internal override void PhysicsUpdate()
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

    protected override void HitKnockback()
    {
        if (hitForcesApplied) return;

        Vector2 theForce = new Vector2(fsm.Agent.KnockbackSystem.KnockbackDirection * fsm.Agent.Data.KnockbackForce.x, fsm.Agent.Data.KnockbackForce.y);

        fsm.Agent.Rb2d.AddForce(theForce, ForceMode2D.Impulse);

        hitForcesApplied = true;
    }

}
