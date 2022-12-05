using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using static Codice.Client.Common.WebApi.WebApiEndpoints;

public class ClimbState : State
{
    protected override void EnterState()
    {
        // The Kinematic body Type will avoid collissions, this way the player will be able to climb on top of the collider
        // While we could use Gravity Scale 0, it will have problems when trying to stay on top of the ladder
        

        fsm.Agent.Rb2d.bodyType = RigidbodyType2D.Kinematic;
        fsm.Agent.Rb2d.velocity = Vector2.zero;
        
        fsm.Agent.AnimationManager.PlayAnimation(AnimationType.climb);

        PlacePlayerInCenterLadder();
        fsm.Agent.MovementData.ResetJump(fsm.Agent);
    }

    public override void LogicUpdate()
    {
        if (fsm.Agent.Input.MovementVector.magnitude > 0f)
        {
            fsm.Agent.AnimationManager.Resume();
        }
        else
        {
            fsm.Agent.AnimationManager.Pause();
            fsm.Agent.Rb2d.velocity = Vector2.zero;
        }
    }

    public override void PhysicsUpdate()
    {
        fsm.Agent.GroundDetector.CheckIsGroundedWhileClimbing();
        fsm.Agent.WallDetector.CheckIsTouchingWall();
        fsm.Agent.ClimbingDetector.CheckIfCanClimb();

        fsm.Agent.Rb2d.velocity =  fsm.Agent.Input.MovementVector.y * fsm.Agent.Data.ClimbSpeed * Vector2.up;

        if (fsm.Agent.GroundDetector.IsGrounded && 
            fsm.Agent.ClimbingDetector.TopLadder != null && 
            fsm.Agent.Input.MovementVector.y < 0f)
        {
            fsm.TransitionToState(fsm.StateFactory.GetState(StateType.Idle));
        }

        if (!fsm.Agent.ClimbingDetector.CanClimb)
        {
            fsm.TransitionToState(fsm.StateFactory.GetState(StateType.Idle));
        }
    }

    protected override void ExitState()
    {
        fsm.Agent.AnimationManager.Resume();
        fsm.Agent.Rb2d.bodyType = RigidbodyType2D.Dynamic;
        fsm.Agent.MovementData.ResetJump(fsm.Agent);
        // EndClimb();
    }

    private void PlacePlayerInCenterLadder()
    {
        fsm.Agent.Rb2d.position = new Vector2(fsm.Agent.ClimbingDetector.Ladder.bounds.center.x, fsm.Agent.Rb2d.position.y);
    }

}
