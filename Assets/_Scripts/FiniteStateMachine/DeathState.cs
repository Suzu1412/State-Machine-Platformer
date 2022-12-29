using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathState : State
{
    protected override void EnterState()
    {
        fsm.Agent.AnimationManager.PlayAnimation(AnimationType.death);
    }

    public override void LogicUpdate()
    {
    }

    public override void PhysicsUpdate()
    {
    }

    protected override void ExitState()
    {
        fsm.Agent.Respawn.RespawnFromCheckPoint();
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
