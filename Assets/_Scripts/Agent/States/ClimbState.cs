using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbState : State
{
    [SerializeField] private State idleState;

    protected override void EnterState()
    {
        // The Kinematic body Type will avoid collissions, this way the player will be able to climb on top of the collider
        // While we could use Gravity Scale 0, it will have problems when trying to stay on top of the ladder
        agent.Rb2d.bodyType = RigidbodyType2D.Kinematic; 
        agent.Rb2d.velocity = Vector2.zero;
        ResetJump();
        agent.AnimationManager.PlayAnimation(AnimationType.climb);
        
    }

    public override void StateUpdate()
    {
        if (agent.Input.MovementVector.magnitude > 0f)
        {
            agent.AnimationManager.Resume();
        }
        else
        {
            agent.AnimationManager.Pause();
            agent.Rb2d.velocity = Vector2.zero;
        }
    }

    public override void StateFixedUpdate()
    {
        DetectCollissions();

        agent.Rb2d.velocity = Vector2.up * agent.Input.MovementVector.y * agent.Data.ClimbSpeed;

        /*
        if (agent.GroundDetector.IsGrounded && agent.Input.MovementVector.y < 0f)
        {
            agent.TransitionToState(idleState);
        }
        */

        if (!CheckIfCanClimb())
        {
            agent.TransitionToState(idleState);
        }
    }

    protected override void ExitState()
    {
        agent.AnimationManager.Resume();
        agent.Rb2d.bodyType = RigidbodyType2D.Dynamic;
        ConsumeJump();
        EndClimb();
    }

    protected override void HandleMovement(Vector2 movement)
    {
        
    }
}
