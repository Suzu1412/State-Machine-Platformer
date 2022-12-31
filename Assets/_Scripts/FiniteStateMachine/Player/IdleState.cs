using UnityEngine;


public class IdleState : State
{
    protected override void EnterState()
    {
        base.EnterState();
        fsm.Agent.AnimationManager.PlayAnimation(AnimationType.idle);
        fsm.Agent.Rb2d.velocity = Vector2.zero;
    }

    public override void LogicUpdate()
    {
        if (!fsm.Agent.CollissionSenses.IsGrounded)
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

        if (Mathf.Abs(fsm.Agent.Input.MovementVector.x) > 0f && 
            !fsm.Agent.CollissionSenses.IsTouchingWall)
        {
            fsm.TransitionToState(StateType.Move);
        }
    }

    protected override void HandleMovement(Vector2 input)
    {
        if (input.y > 0.33f)
        {
            if (fsm.Agent.CollissionSenses.IsTouchingLadder && fsm.Agent.CollissionSenses.TopLadder == null)
            {
                fsm.TransitionToState(StateType.Climb);
            }
        }

        if (input.y < -0.33f)
        {
            if (fsm.Agent.CollissionSenses.IsTouchingLadder && fsm.Agent.CollissionSenses.TopLadder != null)
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
}
