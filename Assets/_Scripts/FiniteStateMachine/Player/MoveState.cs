using UnityEngine;
using UnityEngine.Events;


public class MoveState : State
{
    public UnityEvent OnStep;

    private void OnValidate()
    {
        stateType= StateType.Move;
    }

    internal override void EnterState()
    {
        fsm.Agent.AnimationManager.ResetEvents();

        if (!fsm.Agent.AgentWeapon.IsAttacking)
        {
            fsm.Agent.AnimationManager.PlayAnimation(AnimationType.run);

            fsm.Agent.MovementData.SetHorizontalMovementDirection(0);
            fsm.Agent.MovementData.SetCurrentSpeed(0f);
            fsm.Agent.MovementData.SetCurrentVelocity(Vector2.zero);
        }
        
        fsm.Agent.AnimationManager.OnAnimationAction.AddListener(() => OnStep.Invoke());

        fsm.Agent.AnimationManager.OnAnimationAttackPerformed.AddListener(() => PerformAttack());
        fsm.Agent.AnimationManager.OnAnimationEnd.AddListener(() => OnAttackEnd());
    }

    internal override void LogicUpdate()
    { 
        HandleAttackTransition();

        CalculateVelocity();
    }

    internal override void PhysicsUpdate()
    {
        SetPlayerVelocity();
    }

    internal override void ExitState()
    {
        fsm.Agent.AnimationManager.OnAnimationAction?.RemoveListener(PerformAttack);
        fsm.Agent.AnimationManager.OnAnimationEnd.AddListener(() => OnAttackEnd());
        fsm.Agent.AnimationManager.ResetEvents();
    }

    protected virtual void CalculateVelocity()
    {
        CalculateSpeed(fsm.Agent.Input.MovementVector);
        CalculateHorizontalDirection();
        fsm.Agent.MovementData.SetCurrentVelocity(fsm.Agent.MovementData.CurrentSpeed * fsm.Agent.MovementData.HorizontalMovementDirection * Vector3.right);
        fsm.Agent.MovementData.SetCurrentVelocity(new Vector2(fsm.Agent.MovementData.CurrentVelocity.x, Mathf.Clamp(fsm.Agent.Rb2d.velocity.y, fsm.Agent.Data.MaxFallSpeed, 30f)));
    }

    protected virtual void CalculateHorizontalDirection()
    {
        if (fsm.Agent.Input.MovementVector.x > 0)
        {
            fsm.Agent.MovementData.SetHorizontalMovementDirection(1);
        }
        else if (fsm.Agent.Input.MovementVector.x < 0)
        {
            fsm.Agent.MovementData.SetHorizontalMovementDirection(-1);
        }
    }

    protected virtual void CalculateSpeed(Vector2 movementVector)
    {
        if (Mathf.Abs(movementVector.x) > 0)
        {
            fsm.Agent.MovementData.SetCurrentSpeed(fsm.Agent.MovementData.CurrentSpeed + fsm.Agent.Data.Acceleration * Time.deltaTime);
        }
        else
        {
            fsm.Agent.MovementData.SetCurrentSpeed(fsm.Agent.MovementData.CurrentSpeed - fsm.Agent.Data.Deacceleration * Time.deltaTime);
        }

        fsm.Agent.MovementData.SetCurrentSpeed(Mathf.Clamp(fsm.Agent.MovementData.CurrentSpeed, 0, fsm.Agent.Data.MaxSpeed));
    }

    protected void SetPlayerVelocity()
    {
        fsm.Agent.Rb2d.velocity = fsm.Agent.MovementData.CurrentVelocity;
    }

    protected override void OnAttackEnd()
    {
        base.OnAttackEnd();

        fsm.Agent.AnimationManager.PlayAnimation(AnimationType.run);
    }
}
