using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallState : MoveState
{
    [SerializeField] private State moveState;
    private bool isInCoyoteTime;
    private float coyoteTimeDuration;

    protected override void EnterState()
    {
        if (CanEnterCoyoteTime)
        {
            isInCoyoteTime = true;
            coyoteTimeDuration = agent.Data.CoyoteDuration;
        }

        agent.AnimationManager.PlayAnimation(AnimationType.fall);
        movementData.SetCurrentVelocity(agent.Rb2d.velocity);
        agent.Rb2d.velocity = movementData.CurrentVelocity;
    }

    public override void StateUpdate()
    {
        base.StateUpdate();
        if (isInCoyoteTime)
        {
            coyoteTimeDuration -= Time.deltaTime;

            if (coyoteTimeDuration <= 0f)
            {
                isInCoyoteTime = false;
                ConsumeJump();
            }
        }
    }


    public override void StateFixedUpdate()
    {
        DetectCollissions();
        SetPlayerVelocity();

        if (agent.GroundDetector.IsGrounded)
        {
            if (agent.Rb2d.velocity.y < 0) return; 
            if (Mathf.Abs(agent.Rb2d.velocity.x) > 0f)
            {
                agent.TransitionToState(moveState);
            }
            else
            {
                agent.TransitionToState(idleState);
            }
        }
    }

    protected override void ExitState()
    {
        ResetJump();
    }

    protected override void CalculateVelocity()
    {
        CalculateSpeed(agent.Input.MovementVector, movementData);
        CalculateHorizontalDirection(movementData);
        movementData.SetCurrentVelocity(Vector3.right * movementData.HorizontalMovementDirection * movementData.CurrentSpeed);
        movementData.SetCurrentVelocity(new Vector2(movementData.CurrentVelocity.x, Mathf.Clamp(agent.Rb2d.velocity.y, agent.Data.MaxFallSpeed, 30f)));
    }
}
