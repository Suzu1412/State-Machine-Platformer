using UnityEngine;

public class ClimbState : State
{
    private bool isGettingOnTopLadder;

    internal override void EnterState()
    {
        // The Kinematic body Type will avoid collissions, this way the player will be able to climb on top of the collider
        // While we could use Gravity Scale 0, it will have problems when trying to stay on top of the ladder
        fsm.Agent.Rb2d.bodyType = RigidbodyType2D.Kinematic;
        fsm.Agent.Rb2d.velocity = Vector2.zero;
        fsm.Agent.CollissionSenses.GroundDetector.IsClimbing = true;
        
        fsm.Agent.AnimationManager.PlayAnimation(AnimationType.climb);

        PlacePlayerInCenterLadder();
        fsm.Agent.MovementData.ResetJump(fsm.Agent);
    }

    internal override void LogicUpdate()
    {
        if (Mathf.Abs(fsm.Agent.Input.MovementVector.y) > 0f)
        {
            fsm.Agent.AnimationManager.Resume();
        }
        else
        {
            fsm.Agent.AnimationManager.Pause();
            fsm.Agent.Rb2d.velocity = Vector2.zero;
        }
    }

    internal override void PhysicsUpdate()
    {
        MoveInLadder();

        //if (fsm.Agent.Input.MovementVector.y > 0)
        //{
        //    MoveToTopLadder();
        //}

        //if (fsm.Agent.Input.MovementVector.y < 0)
        //{
        //    MoveToGround();
        //}

        //if (!fsm.Agent.CollissionSenses.IsTouchingLadder)
        //{
        //    fsm.TransitionToState(StateType.Idle);
        //}
    }

    internal override void ExitState()
    {
        fsm.Agent.CollissionSenses.GroundDetector.IsClimbing = false;
        fsm.Agent.AnimationManager.Resume();
        fsm.Agent.Rb2d.bodyType = RigidbodyType2D.Dynamic;
        fsm.Agent.MovementData.ResetJump(fsm.Agent);

        if (fsm.Agent.CollissionSenses.TopLadder == null) return;

        fsm.Agent.Rb2d.position = new Vector2(fsm.Agent.CollissionSenses.Ladder.bounds.center.x, fsm.Agent.CollissionSenses.TopLadder.transform.parent.position.y + (fsm.Agent.CollissionSenses.AgentCollider.bounds.size.y + 0.1f / 2));

        fsm.Agent.Rb2d.velocity = Vector2.zero;
    }

    private void PlacePlayerInCenterLadder()
    {
        fsm.Agent.Rb2d.position = new Vector2(fsm.Agent.CollissionSenses.Ladder.bounds.center.x, fsm.Agent.Rb2d.position.y);
    }

    private void MoveInLadder()
    {
        if (isGettingOnTopLadder) return;

        fsm.Agent.Rb2d.velocity = fsm.Agent.Input.MovementVector.y * fsm.Agent.Data.ClimbSpeed * Vector2.up;
    }

    private void MoveToTopLadder()
    {
        if (fsm.Agent.CollissionSenses.IsAboveOfTopLadder) return;

        if (fsm.Agent.CollissionSenses.TopLadder == null) return;

        fsm.Agent.Rb2d.position = new Vector2(fsm.Agent.CollissionSenses.Ladder.bounds.center.x, fsm.Agent.CollissionSenses.TopLadder.transform.parent.position.y + (fsm.Agent.CollissionSenses.AgentCollider.bounds.size.y + 0.1f / 2));

        fsm.Agent.Rb2d.velocity = Vector2.zero;

        isGettingOnTopLadder = true;

        fsm.TransitionToState(StateType.Idle);
    }

    private void MoveToGround()
    {
        //if (!fsm.Agent.CollissionSenses.IsGrounded) return;

        //if (fsm.Agent.CollissionSenses.TopLadder != null) return;

        //fsm.TransitionToState(StateType.Idle);
    }
}
