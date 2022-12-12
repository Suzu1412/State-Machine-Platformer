using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollState : MoveState
{
    float duration;

    protected override void EnterState()
    {
        fsm.Agent.CollissionSenses.SetAgentCollider(true);
        fsm.Agent.AnimationManager.PlayAnimation(AnimationType.roll);
        duration = fsm.Agent.Data.RollDuration;
    }

    public override void LogicUpdate()
    {
        duration -= Time.deltaTime;

        if (duration <= 0f)
        {
            fsm.TransitionToState(StateType.Idle);
        }

        if (!fsm.Agent.CollissionSenses.IsGrounded)
        {
            fsm.TransitionToState(StateType.Fall);
        }

        if (fsm.Agent.CollissionSenses.IsTouchingWall)
        {
            fsm.TransitionToState(StateType.Idle);
        }

        CalculateVelocity();
    }

    public override void PhysicsUpdate()
    {
        fsm.Agent.CollissionSenses.DetectGround();
        fsm.Agent.CollissionSenses.DetectWall();

        SetPlayerVelocity();
    }

    protected override void ExitState()
    {
        fsm.Agent.CollissionSenses.SetAgentCollider(false);
    }

    protected override void HandleRollReleased()
    {
        duration = 0f;
    }

    protected override void CalculateVelocity()
    {
        CalculateSpeed(fsm.Agent.Input.MovementVector);
        CalculateHorizontalDirection();
        fsm.Agent.MovementData.SetCurrentVelocity(fsm.Agent.MovementData.CurrentSpeed * fsm.Agent.MovementData.HorizontalMovementDirection * Vector3.right);
        fsm.Agent.MovementData.SetCurrentVelocity(new Vector2(fsm.Agent.MovementData.CurrentVelocity.x, Mathf.Clamp(fsm.Agent.Rb2d.velocity.y, fsm.Agent.Data.MaxFallSpeed, 30f)));
    }

    protected override void CalculateSpeed(Vector2 movementVector)
    {
        fsm.Agent.MovementData.SetCurrentSpeed(fsm.Agent.MovementData.CurrentSpeed + fsm.Agent.Data.RollAcceleration * Time.deltaTime);

        fsm.Agent.MovementData.SetCurrentSpeed(Mathf.Clamp(fsm.Agent.MovementData.CurrentSpeed, 0, fsm.Agent.Data.MaxRollingSpeed));
    }

    protected override void CalculateHorizontalDirection()
    {
        if (fsm.Agent.MovementData.HorizontalMovementDirection == 0)
        {
            fsm.Agent.MovementData.SetHorizontalMovementDirection(1);
        }

        if (fsm.Agent.Input.MovementVector.x > 0)
        {
            fsm.Agent.MovementData.SetHorizontalMovementDirection(1);
        }
        else if (fsm.Agent.Input.MovementVector.x < 0)
        {
            fsm.Agent.MovementData.SetHorizontalMovementDirection(-1);
        }
    }
}
