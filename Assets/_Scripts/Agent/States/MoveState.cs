using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : State
{
    [SerializeField] private MovementData movementData;
    [SerializeField] private State idleState;

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
        base.StateFixedUpdate();
        SetPlayerVelocity(movementData);

        if (Mathf.Abs(agent.Rb2d.velocity.x) < 0.01f || agent.Senses.IsTouchingWall)
        {
            agent.TransitionToState(idleState);
        }

    }

    private void CalculateVelocity()
    {
        CalculateSpeed(agent.Input.MovementVector, movementData);
        CalculateHorizontalDirection(movementData);
        movementData.SetCurrentVelocity(Vector3.right * movementData.HorizontalMovementDirection * movementData.CurrentSpeed);
        movementData.SetCurrentVelocity(new Vector2(movementData.CurrentVelocity.x, agent.Rb2d.velocity.y));
    }

    private void CalculateHorizontalDirection(MovementData data)
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

    private void CalculateSpeed(Vector2 movementVector, MovementData data)
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

    private void SetPlayerVelocity(MovementData movementData)
    {
        agent.Rb2d.velocity = movementData.CurrentVelocity;
    }
}
