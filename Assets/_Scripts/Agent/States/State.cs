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
    [SerializeField] protected State climbState;
    private int jumpPressed;
    [SerializeField] private int amountOfJumps;
    private bool isJumping;
    private bool isClimbing;
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
        DetectCollissions();
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
        if (Mathf.Abs(movement.y) > 0.5f)
        {
            ActivateClimb();
        }
    }

    protected virtual void HandleJumpPressed()
    {
        ActivateJump();
    }

    protected virtual void HandleJumpReleased()
    {
        EndJump();
    }

    #endregion

    protected bool CheckIfCanClimb()
    {
        if (!agent.ClimbingDetector.CanClimb) return false;

        return true;
    }

    private void ActivateClimb()
    {
        if (!CheckIfCanClimb()) return;

        isClimbing = true;

        agent.TransitionToState(climbState);
    }

    protected void EndClimb()
    {
        isClimbing = false;
    }

    /// <summary>
    /// The same method as the activate Jump but without the grounded condition
    /// </summary>
    protected void ActivateJumpClimbing()
    {
        ConsumeJump();
        canEnterCoyoteTime = false;

        isJumping = true;

        agent.TransitionToState(jumpState);
    }

    #region Jump Action
    private bool CheckIfCanJump()
    {
        if (isJumping) return false;
        if (amountOfJumps <= 0) return false;

        return true;
    }

    protected void ActivateJump()
    {
        if (!CheckIfCanJump()) return;

        ConsumeJump();
        canEnterCoyoteTime = false;

        isJumping = true;

        agent.TransitionToState(jumpState);
    }

    protected void ResetJump()
    {
        if (!agent.GroundDetector.IsGrounded && !isClimbing) return;
        isJumping = false;
        canEnterCoyoteTime = true;
        amountOfJumps = agent.Data.AmountOfJumps;
    }

    protected void ConsumeJump()
    {
        amountOfJumps--;
    }

    private void EndJump()
    {
        isJumping = false;
    }
    #endregion

    protected void DetectCollissions()
    {
        agent.GroundDetector.CheckIsGrounded();
        agent.WallDetector.CheckIsTouchingWall();
        agent.ClimbingDetector.CheckIfCanClimb();
    }
}
