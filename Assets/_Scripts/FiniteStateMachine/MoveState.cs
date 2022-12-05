using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using static Codice.Client.Common.WebApi.WebApiEndpoints;

public class MoveState : State
{
    protected override void EnterState()
    {
        fsm.Agent.AnimationManager.PlayAnimation(AnimationType.run);
        fsm.Agent.MovementData.SetHorizontalMovementDirection(0);
        fsm.Agent.MovementData.SetCurrentSpeed(0f);
        fsm.Agent.MovementData.SetCurrentVelocity(Vector2.zero);
    }

    public override void LogicUpdate()
    {
        CalculateVelocity();

        if (!fsm.Agent.GroundDetector.IsGrounded)
        {
            fsm.TransitionToState(fsm.StateFactory.GetState(StateType.Fall));
        }
    }

    public override void PhysicsUpdate()
    {
        fsm.Agent.GroundDetector.CheckIsGrounded();
        fsm.Agent.WallDetector.CheckIsTouchingWall();
        fsm.Agent.ClimbingDetector.CheckIfCanClimb();

        SetPlayerVelocity();

        if (Mathf.Abs(fsm.Agent.Rb2d.velocity.x) < 0.01f || fsm.Agent.WallDetector.IsTouchingWall)
        {
            fsm.TransitionToState(fsm.StateFactory.GetState(StateType.Idle));
        }
    }

    protected virtual void CalculateVelocity()
    {
        CalculateSpeed(fsm.Agent.Input.MovementVector);
        CalculateHorizontalDirection();
        fsm.Agent.MovementData.SetCurrentVelocity(fsm.Agent.MovementData.CurrentSpeed * fsm.Agent.MovementData.HorizontalMovementDirection * Vector3.right);
        fsm.Agent.MovementData.SetCurrentVelocity(new Vector2(fsm.Agent.MovementData.CurrentVelocity.x, Mathf.Clamp(fsm.Agent.Rb2d.velocity.y, fsm.Agent.Data.MaxFallSpeed, 30f)));
    }

    protected void CalculateHorizontalDirection()
    {
        if (fsm.Agent.Input.MovementVector.x > 0)
        {
            fsm.Agent.MovementData.SetHorizontalMovementDirection(1);
        }
        else if (fsm.Agent.Input.MovementVector.x < 0)
        {
            fsm.Agent.MovementData.SetHorizontalMovementDirection(-1);
        }
    }

    protected void CalculateSpeed(Vector2 movementVector)
    {
        if (Mathf.Abs(movementVector.x) > 0)
        {
            fsm.Agent.MovementData.SetCurrentSpeed(fsm.Agent.MovementData.CurrentSpeed + fsm.Agent.Data.Acceleration * Time.deltaTime);
        }
        else
        {
            fsm.Agent.MovementData.SetCurrentSpeed(fsm.Agent.MovementData.CurrentSpeed - fsm.Agent.Data.Deacceleration * Time.deltaTime);
        }

        fsm.Agent.MovementData.SetCurrentSpeed(Mathf.Clamp(fsm.Agent.MovementData.CurrentSpeed, 0, fsm.Agent.Data.MaxSpeed));
    }

    protected void SetPlayerVelocity()
    {
        fsm.Agent.Rb2d.velocity = fsm.Agent.MovementData.CurrentVelocity;
    }
}
