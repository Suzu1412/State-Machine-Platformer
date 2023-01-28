using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AIPatrollingState : State
{
    public UnityEvent OnStep;
    private Vector2 direction = Vector2.zero;

    internal override void EnterState()
    {
        fsm.Agent.AnimationManager.PlayAnimation(AnimationType.run);
        fsm.Agent.AnimationManager.OnAnimationAction.AddListener(() => OnStep.Invoke());

        fsm.Agent.MovementData.SetHorizontalMovementDirection(0);
        fsm.Agent.MovementData.SetCurrentSpeed(0f);
        fsm.Agent.MovementData.SetCurrentVelocity(Vector2.zero);

        if (fsm.Agent.Input.MovementVector.x == 0f)
        {
            direction.Set(-1f, 0f);
        } 
        fsm.Agent.Input.CallOnMovementVector(direction);
    }

    internal override void LogicUpdate()
    {
        CalculateVelocity();

        if (!fsm.Agent.CollissionSenses.IsGrounded && fsm.Agent.Rb2d.velocity.y < 0f)
        {
            fsm.TransitionToState(StateType.Fall);
        }
    }

    internal override void PhysicsUpdate()
    {
        SetPlayerVelocity();

        if (fsm.Agent.CollissionSenses.IsTouchingWall || !fsm.Agent.CollissionSenses.IsThereGroundAhead)
        {
            direction.Set(direction.x * -1, 0f);
            fsm.TransitionToState(StateType.Idle);
        }

        if (fsm.Agent.AgentWeapon.GetCurrentWeapon().CheckIfTargetInRange(fsm.Agent.AgentWeapon.transform, fsm.Agent.Data.HittableLayerMask, direction))
        {
            fsm.TransitionToState(StateType.Attack);
        }
    }


    internal override void ExitState()
    {
        fsm.Agent.AnimationManager.ResetEvents();
    }

    protected virtual void CalculateVelocity()
    {
        CalculateSpeed(fsm.Agent.Input.MovementVector);
        CalculateHorizontalDirection();
        fsm.Agent.MovementData.SetCurrentVelocity(fsm.Agent.MovementData.CurrentSpeed * fsm.Agent.MovementData.HorizontalMovementDirection * Vector3.right);
        fsm.Agent.MovementData.SetCurrentVelocity(new Vector2(fsm.Agent.MovementData.CurrentVelocity.x, Mathf.Clamp(fsm.Agent.Rb2d.velocity.y, fsm.Agent.Data.MaxFallSpeed, 30f)));
    }

    protected virtual void CalculateHorizontalDirection()
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

    protected virtual void CalculateSpeed(Vector2 movementVector)
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
