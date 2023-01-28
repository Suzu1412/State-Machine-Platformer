using UnityEngine;


public class IdleState : State
{
    private void OnValidate()
    {
        stateType = StateType.Idle;
    }

    internal override void EnterState()
    {
        base.EnterState();
        fsm.Agent.AnimationManager.PlayAnimation(AnimationType.idle);
        fsm.Agent.Rb2d.velocity = Vector2.zero;
    }

    internal override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    internal override void PhysicsUpdate()
    {
    }

    protected override void HandleMovement(Vector2 input)
    {
        //if (input.y > 0.33f)
        //{
        //    if (fsm.Agent.CollissionSenses.IsTouchingLadder && fsm.Agent.CollissionSenses.TopLadder == null)
        //    {
        //        fsm.TransitionToState(StateType.Climb);
        //    }
        //}

        //if (input.y < -0.33f)
        //{
        //    if (fsm.Agent.CollissionSenses.IsTouchingLadder && fsm.Agent.CollissionSenses.TopLadder != null)
        //    {
        //        fsm.TransitionToState(StateType.Climb);
        //    }
        //}
    }

    protected override void HandleRollPressed()
    {
        //if (fsm.Agent.CollissionSenses.IsTouchingWall) return;

        //fsm.TransitionToState(StateType.Roll);
    }
}
