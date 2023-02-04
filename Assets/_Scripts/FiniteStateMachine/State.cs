using UnityEngine;
using UnityEngine.Events;

public abstract class State : MonoBehaviour
{
    [SerializeField] private StateTransitionSO stateTransition;
    [SerializeField] protected StateType stateType;
    protected FiniteStateMachine fsm;
    public UnityEvent<AudioClip> OnWeaponSound;
    public UnityEvent OnEnter, OnExit;
    protected Vector2 movement;
    public StateType StateType => stateType;

    public void InitializeState(FiniteStateMachine fsm)
    {
        this.fsm = fsm;
    }

    internal void Enter()
    {
        fsm.Agent.Input.OnMovement += HandleMovement;
        fsm.Agent.Input.OnMovement += HandleFaceDirection;
        fsm.Agent.Input.OnJumpPressed += HandleJumpPressed;
        fsm.Agent.Input.OnJumpReleased += HandleJumpReleased;
        fsm.Agent.Input.OnRollPressed += HandleRollPressed;
        fsm.Agent.Input.OnRollReleased += HandleRollReleased;
        fsm.Agent.Input.OnAttackPressed += HandleAttackPressed;
        fsm.Agent.HealthSystem.OnHit += Hit;
        fsm.Agent.KnockbackSystem.OnHitKnockback += HitKnockback;
        fsm.Agent.HealthSystem.OnDeath += Death;
        OnEnter?.Invoke();
        EnterState();
    }

    internal void Exit()
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
        fsm.Agent.KnockbackSystem.OnHitKnockback -= HitKnockback;
        OnExit?.Invoke();
        ExitState();
    }

    internal virtual void EnterState()
    {
        
    }

    internal virtual void LogicUpdate()
    {
    }

    internal virtual void PhysicsUpdate()
    {
    }

    internal virtual void ExitState()
    {

    }

    internal void TransitionToState(FiniteStateMachine fsm)
    {
        if (stateTransition == null) return;

        stateTransition.HandleTransition(fsm);
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
        //if (fsm.Agent.MovementData.AmountOfJumps > 0) 
        //{
        //    fsm.TransitionToState(StateType.Jump);
        //}
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
        //if (fsm.Agent.AgentWeapon.CanIUseWeapon())
        //{
        //    fsm.TransitionToState(StateType.Attack);
        //}
    }

    protected virtual void Hit()
    {
        fsm.TransitionToState(StateType.Hit);
    }

    protected virtual void Death()
    {
        fsm.TransitionToState(StateType.Death);
    }

    protected virtual void HitKnockback()
    {

    }

    protected virtual void HandleAttackTransition()
    {
        if (!fsm.Agent.AgentWeapon.IsAttacking && fsm.Agent.Input.AttackPressed)
        {
            fsm.Agent.AgentWeapon.TryPerformAttack(fsm.Agent.CollissionSenses.IsGrounded);
        }

        if (fsm.Agent.AgentWeapon.IsAttacking)
        {
            fsm.Agent.AnimationManager.PlayAnimation(AnimationType.attack);
        }
    }

    protected virtual void PerformAttack()
    {
        OnWeaponSound?.Invoke(fsm.Agent.AgentWeapon.GetCurrentWeapon().WeaponSwingSound);

        fsm.Agent.AgentWeapon.PerformAttack(fsm.Agent.Data.HittableLayerMask);
    }

    protected virtual void OnAttackEnd()
    {
        fsm.Agent.AgentWeapon.ToggleWeaponVisibility(false);
        fsm.Agent.AgentWeapon.ResetAttack();
    }

    protected virtual Vector2 LookAtClosestTarget()
    {
        Vector2 direction = Vector2.zero;

        if (direction.x == 0f)
        {
            direction.Set(-1f, 0f);
        }

        if (fsm.Agent.CollissionSenses.TargetDetector.Target == null) return Vector2.zero;

        if (fsm.Agent.CollissionSenses.TargetDetector.DirectionToTarget.x > 1f)
        {
            direction.Set(1f, 0f);
            fsm.Agent.Input.CallOnMovementVector(direction);
        }
        else if (fsm.Agent.CollissionSenses.TargetDetector.DirectionToTarget.x < 1f)
        {
            direction.Set(-1f, 0f);
            fsm.Agent.Input.CallOnMovementVector(direction);
        }

        return direction;
    }
    #endregion
}
