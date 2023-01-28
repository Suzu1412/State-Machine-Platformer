using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AttackState : State 
{
    public UnityEvent<AudioClip> OnWeaponSound;

    internal override void EnterState()
    {
        fsm.Agent.AnimationManager.ResetEvents();
        fsm.Agent.AnimationManager.PlayAnimation(AnimationType.attack);
        fsm.Agent.AnimationManager.OnAnimationAction.AddListener(() => PerformAttack());
        fsm.Agent.AnimationManager.OnAnimationEnd.AddListener(() => OnAttackEnd());

        fsm.Agent.AgentWeapon.ToggleWeaponVisibility(true);

        fsm.Agent.Rb2d.velocity = Vector2.zero;
    }

    internal override void PhysicsUpdate()
    {
    }

    internal override void ExitState()
    {
        fsm.Agent.AnimationManager.ResetEvents();
        fsm.Agent.AgentWeapon.ToggleWeaponVisibility(false);
    }

    protected override void HandleJumpPressed()
    {
    }

    protected override void HandleAttackPressed()
    {
    }

    protected override void HandleRollPressed()
    {
    }

    private void PerformAttack()
    {
        OnWeaponSound?.Invoke(fsm.Agent.AgentWeapon.GetCurrentWeapon().WeaponSwingSound);
        fsm.Agent.AnimationManager.OnAnimationAction?.RemoveListener(PerformAttack);
        fsm.Agent.AgentWeapon.PerformAttack(fsm.Agent.Data.HittableLayerMask);
    }

    private void OnAttackEnd()
    {
        if (Mathf.Abs(fsm.Agent.Input.MovementVector.x) > 0f &&
            !fsm.Agent.CollissionSenses.IsTouchingWall)
        {
            fsm.TransitionToState(StateType.Move);
        }
        else
        {
            fsm.TransitionToState(StateType.Idle);
        }
    }
}
