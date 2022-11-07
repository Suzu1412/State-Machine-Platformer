using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : State
{
    [SerializeField] protected MovementData movementData;
    [SerializeField] protected State idleState;

    protected override void EnterState()
    {
        agent.AnimationManager.PlayAnimation(AnimationType.run);
        movementData.SetHorizontalMovementDirection(0);
        movementData.SetCurrentSpeed(0f);
        movementData.SetCurrentVelocity(Vector2.zero);
    }

    public override void StateUpdate()
    {
        base.StateUpdate();
        CalculateVelocity();
    }

    public override void StateFixedUpdate()
    {
        DetectCollissions();
        SetPlayerVelocity();

        if (Mathf.Abs(agent.Rb2d.velocity.x) < 0.01f || agent.WallDetector.IsTouchingWall)
        {
            agent.TransitionToState(idleState);
        }

        if (!agent.GroundDetector.IsGrounded)
        {
            agent.TransitionToState(fallState);
        }

    }

    protected virtual void CalculateVelocity()
    {
        CalculateSpeed(agent.Input.MovementVector, movementData);
        CalculateHorizontalDirection(movementData);
        movementData.SetCurrentVelocity(Vector3.right * movementData.HorizontalMovementDirection * movementData.CurrentSpeed);
        movementData.SetCurrentVelocity(new Vector2(movementData.CurrentVelocity.x, Mathf.Clamp(agent.Rb2d.velocity.y, agent.Data.MaxFallSpeed, 30f)));
    }

    protected void CalculateHorizontalDirection(MovementData data)
    {
        if (agent.Input.MovementVector.x > 0)
        {
            data.SetHorizontalMovementDirection(1);
        }
        else if (agent.Input.MovementVector.x < 0)
        {
            data.SetHorizontalMovementDirection(-1);
        }
    }

    protected void CalculateSpeed(Vector2 movementVector, MovementData data)
    {
        if (Mathf.Abs(movementVector.x) > 0)
        {
            data.SetCurrentSpeed(data.CurrentSpeed + agent.Data.Acceleration * Time.deltaTime);
        }
        else
        {
            data.SetCurrentSpeed(data.CurrentSpeed - agent.Data.Deacceleration * Time.deltaTime);
        }
        movementData.SetCurrentSpeed(Mathf.Clamp(data.CurrentSpeed, 0, agent.Data.MaxSpeed));
    }

    protected void SetPlayerVelocity()
    {
        agent.Rb2d.velocity = movementData.CurrentVelocity;
    }
}
