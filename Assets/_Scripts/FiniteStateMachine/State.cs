using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static Codice.Client.Common.WebApi.WebApiEndpoints;

public abstract class State : MonoBehaviour
{
    protected FiniteStateMachine fsm;
    public UnityEvent OnEnter, OnExit;
    protected Vector2 movement;

    public void InitializeState(FiniteStateMachine fsm)
    {
        this.fsm = fsm;
    }

    public void Enter()
    {
        fsm.Agent.Input.OnMovement += HandleMovement;
        fsm.Agent.Input.OnMovement += fsm.Agent.RendererManager.FaceDirection;
        fsm.Agent.Input.OnJumpPressed += HandleJumpPressed;
        fsm.Agent.Input.OnJumpReleased += HandleJumpReleased;
        OnEnter?.Invoke();
        EnterState();
    }

    public void Exit()
    {
        fsm.Agent.Input.OnMovement -= HandleMovement;
        fsm.Agent.Input.OnMovement -= fsm.Agent.RendererManager.FaceDirection;
        fsm.Agent.Input.OnJumpPressed -= HandleJumpPressed;
        fsm.Agent.Input.OnJumpReleased -= HandleJumpReleased;
        OnExit?.Invoke();
        ExitState();
    }

    protected virtual void EnterState()
    {
        
    }

    public virtual void LogicUpdate()
    {
        
    }

    public virtual void PhysicsUpdate()
    {
        fsm.Agent.GroundDetector.CheckIsGrounded();
        fsm.Agent.WallDetector.CheckIsTouchingWall();
        fsm.Agent.ClimbingDetector.CheckIfCanClimb();
    }

    protected virtual void ExitState()
    {

    }

    #region State Override Methods
    protected virtual void HandleMovement(Vector2 movement)
    {
        Debug.Log(movement);


        if (Mathf.Abs(movement.y) > 0.33f)
        {
            if (fsm.Agent.ClimbingDetector.CanClimb)
            {
                fsm.TransitionToState(fsm.StateFactory.GetState(StateType.Climb));
            }
        }
    }

    protected void HandleJumpPressed()
    {
        if (fsm.Agent.MovementData.ActivateJump()) 
        {
            fsm.TransitionToState(fsm.StateFactory.GetState(StateType.Jump));
        }
    }

    protected virtual void HandleJumpReleased()
    {
    }
    #endregion
}
