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

    internal override void ExitState()
    {
        base.ExitState();
    }

    protected override void HitKnockback()
    {
        if (hitForcesApplied) return;

        Vector2 theForce = new Vector2(fsm.Agent.KnockbackSystem.KnockbackDirection * fsm.Agent.Data.KnockbackForce.x, fsm.Agent.Data.KnockbackForce.y);

        Debug.Log(fsm.Agent.KnockbackSystem.KnockbackDirection * fsm.Agent.Data.KnockbackForce.x);

        fsm.Agent.Rb2d.AddForce(theForce, ForceMode2D.Impulse);

        hitForcesApplied = true;
    }

}
