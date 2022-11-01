using System.Collections;
using System.Collections.Generic;
using Unity.Android.Types;
using UnityEngine;
using UnityEngine.Events;

public abstract class State : MonoBehaviour
{
    protected Agent agent;
    [SerializeField] protected State jumpState;
    [SerializeField] protected State fallState;
    private int amountOfJumps;
    private bool isJumping;
    private bool canEnterCoyoteTime;
    
    public UnityEvent OnEnter, OnExit;

    protected Vector2 movement;

    protected bool CanEnterCoyoteTime => canEnterCoyoteTime;

    public void InitializeState(Agent agent)
    {
        this.agent = agent;
        amountOfJumps = agent.Data.AmountOfJumps;
        canEnterCoyoteTime = true;
    }

    #region Handle State
    public void Enter()
    {
        agent.Input.OnMovement += HandleMovement;
        agent.Input.OnMovement += agent.RendererManager.FaceDirection;
        agent.Input.OnJumpPressed += HandleJumpPressed;
        agent.Input.OnJumpReleased += HandleJumpReleased;
        OnEnter?.Invoke();
        EnterState();
    }

    public virtual void StateUpdate()
    {
    }

    public virtual void StateFixedUpdate()
    {
        agent.Senses.CheckIsGrounded();
        agent.Senses.CheckIsTouchingWall();
    }

    public void Exit()
    {
        agent.Input.OnMovement -= HandleMovement;
        agent.Input.OnMovement -= agent.RendererManager.FaceDirection;
        OnExit?.Invoke();
        ExitState();
    }

    #endregion

    #region State override Methods
    protected virtual void EnterState()
    {

    }

    protected virtual void ExitState()
    {
    }

    protected virtual void HandleMovement(Vector2 movement)
    {
    }

    protected virtual void HandleJumpPressed()
    {
        CheckIfCanJump();
    }

    protected virtual void HandleJumpReleased()
    {
        EndJump();
    }

    protected void EndJump()
    {
        isJumping = false;
    }

    
    #endregion

    private void TestJumpTransition()
    {
        if (agent.Senses.IsGrounded)
        {
            agent.TransitionToState(jumpState);
        }
    }

    protected void CheckIfCanJump()
    {
        ActivateJump();
    }

    protected void ActivateJump()
    {
        if (isJumping) return;
        if (amountOfJumps <= 0) return;

        ConsumeJump();
        canEnterCoyoteTime = false;

        isJumping = true;

        agent.TransitionToState(jumpState);
    }

    protected void ResetJump()
    {
        if (!agent.Senses.IsGrounded) return;
        isJumping = false;
        canEnterCoyoteTime = true;
        amountOfJumps = agent.Data.AmountOfJumps;
    }

    protected void ConsumeJump()
    {
        amountOfJumps--;
    }
}
