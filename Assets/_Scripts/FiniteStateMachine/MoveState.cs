using UnityEngine;
using UnityEngine.Events;


public class MoveState : State
{
    public UnityEvent OnStep;

    protected override void EnterState()
    {
        fsm.Agent.AnimationManager.PlayAnimation(AnimationType.run);
        fsm.Agent.AnimationManager.OnAnimationAction.AddListener(() => OnStep.Invoke());

        fsm.Agent.MovementData.SetHorizontalMovementDirection(0);
        fsm.Agent.MovementData.SetCurrentSpeed(0f);
        fsm.Agent.MovementData.SetCurrentVelocity(Vector2.zero);
    }

    public override void LogicUpdate()
    {
        CalculateVelocity();

        if (!fsm.Agent.CollissionSenses.IsGrounded && fsm.Agent.Rb2d.velocity.y < 0f)
        {
            fsm.TransitionToState(StateType.Fall);
        }
    }

    public override void PhysicsUpdate()
    {
        fsm.Agent.CollissionSenses.DetectGround();
        fsm.Agent.CollissionSenses.DetectWall();
        fsm.Agent.CollissionSenses.DetectLadder();
        fsm.Agent.CollissionSenses.DetectIfOnTopOfLadder();

        SetPlayerVelocity();

        if (Mathf.Abs(fsm.Agent.Rb2d.velocity.x) < 0.01f || fsm.Agent.CollissionSenses.IsTouchingWall)
        {
            fsm.TransitionToState(StateType.Idle);
        }

        ClimbLadder();
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

    protected void ClimbLadder()
    {
        if (fsm.Agent.Input.MovementVector.y > 0.33f)
        {
            if (fsm.Agent.CollissionSenses.IsTouchingLadder && 
                fsm.Agent.CollissionSenses.TopLadder == null)
            {
                fsm.TransitionToState(StateType.Climb);
            }
        }

        if (fsm.Agent.Input.MovementVector.y < -0.33f)
        {
            if (fsm.Agent.CollissionSenses.IsTouchingLadder && 
                (fsm.Agent.CollissionSenses.TopLadder != null || 
                !fsm.Agent.CollissionSenses.IsGrounded))
            {
                fsm.TransitionToState(StateType.Climb);
            }
        }
    }

    protected override void HandleRollPressed()
    {
        if (fsm.Agent.CollissionSenses.IsTouchingWall) return;

        fsm.TransitionToState(StateType.Roll);
    }

    protected override void ExitState()
    {
        fsm.Agent.AnimationManager.ResetEvents();
    }
}
