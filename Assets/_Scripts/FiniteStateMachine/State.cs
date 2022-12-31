using UnityEngine;
using UnityEngine.Events;

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
        fsm.Agent.Input.OnMovement += HandleFaceDirection;
        fsm.Agent.Input.OnJumpPressed += HandleJumpPressed;
        fsm.Agent.Input.OnJumpReleased += HandleJumpReleased;
        fsm.Agent.Input.OnRollPressed += HandleRollPressed;
        fsm.Agent.Input.OnRollReleased += HandleRollReleased;
        fsm.Agent.Input.OnAttackPressed += HandleAttackPressed;
        fsm.Agent.HealthSystem.OnHit += Hit;
        fsm.Agent.HealthSystem.OnDeath += Death;
        OnEnter?.Invoke();
        EnterState();
    }

    public void Exit()
    {
        fsm.Agent.Input.OnMovement -= HandleMovement;
        fsm.Agent.Input.OnMovement -= HandleFaceDirection;
        fsm.Agent.Input.OnJumpPressed -= HandleJumpPressed;
        fsm.Agent.Input.OnJumpReleased -= HandleJumpReleased;
        fsm.Agent.Input.OnRollPressed -= HandleRollPressed;
        fsm.Agent.Input.OnRollReleased -= HandleRollReleased;
        fsm.Agent.Input.OnAttackPressed -= HandleAttackPressed;
        fsm.Agent.HealthSystem.OnHit -= Hit;
        fsm.Agent.HealthSystem.OnDeath -= Death;
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
    }

    protected virtual void ExitState()
    {

    }

    #region State Override Methods
    protected virtual void HandleMovement(Vector2 input)
    {
        
    }

    protected virtual void HandleFaceDirection(Vector2 input)
    {
        fsm.Agent.RendererManager.FaceDirection(input);
    }

    protected virtual void HandleJumpPressed()
    {
        if (fsm.Agent.MovementData.AmountOfJumps > 0) 
        {
            fsm.TransitionToState(StateType.Jump);
        }
    }

    protected virtual void HandleJumpReleased()
    {
    }
    
    protected virtual void HandleRollPressed()
    {

    }

    protected virtual void HandleRollReleased()
    {

    }

    protected virtual void HandleAttackPressed()
    {
        if (fsm.Agent.AgentWeapon.CanIUseWeapon())
        {
            fsm.TransitionToState(StateType.Attack);
        }
    }

    protected virtual void Hit()
    {
        fsm.TransitionToState(StateType.Hit);
    }

    protected virtual void Death()
    {
        fsm.TransitionToState(StateType.Death);
    }
    #endregion
}
