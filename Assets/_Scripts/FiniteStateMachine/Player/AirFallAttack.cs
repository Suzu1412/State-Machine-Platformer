using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AirFallAttack : FallState
{
    public UnityEvent<AudioClip> OnWeaponSound;

    private void OnValidate()
    {
        stateType= StateType.AirFallAttack;
    }

    internal override void EnterState()
    {
        fsm.Agent.AnimationManager.ResetEvents();
        fsm.Agent.AnimationManager.PlayAnimation(AnimationType.attack);
        fsm.Agent.AnimationManager.OnAnimationAction.AddListener(() => PerformAttack());
        fsm.Agent.AnimationManager.OnAnimationEnd.AddListener(() => OnAttackEnd());

        fsm.Agent.AgentWeapon.ToggleWeaponVisibility(true);
    }

    internal override void LogicUpdate()
    {
        CalculateVelocity();
    }

    internal override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    internal override void ExitState()
    {
        base.ExitState();
        fsm.Agent.AnimationManager.ResetEvents();
        fsm.Agent.AgentWeapon.ToggleWeaponVisibility(false);
    }

    protected override void HandleTransitionToStates()
    {
        if (fsm.Agent.CollissionSenses.IsGrounded && fsm.Agent.Rb2d.velocity.y == 0f)
        {
            fsm.TransitionToState(StateType.Attack);
        }
    }


    private void OnAttackEnd()
    {
        fsm.TransitionToState(StateType.Fall);
    }


    private void PerformAttack()
    {
        OnWeaponSound?.Invoke(fsm.Agent.AgentWeapon.GetCurrentWeapon().WeaponSwingSound);
        fsm.Agent.AnimationManager.OnAnimationAction?.RemoveListener(PerformAttack);
        fsm.Agent.AgentWeapon.PerformAttack(fsm.Agent.Data.HittableLayerMask);
    }
}
