using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class State : MonoBehaviour
{
    protected Agent agent;

    public UnityEvent OnEnter, OnExit;

    protected Vector2 movement;

    public void InitializeState(Agent agent)
    {
        this.agent = agent;
    }

    #region Handle State
    public void Enter()
    {
        agent.Input.OnMovement += HandleMovement;
        agent.Input.OnMovement += agent.RendererManager.FaceDirection;
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

    #endregion

    /*
    private void HandleMovement(Vector2 movement)
    {
        if (Mathf.Abs(movement.x) > 0)
        {
            movement.Set(movement.x * 5, agent.Rb2d.velocity.y);
        }
        else
        {
            movement.Set(0f, agent.Rb2d.velocity.y);
        }

        agent.Rb2d.velocity = movement;
    }
    */
}
