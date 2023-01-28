using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AirJumpAttack : JumpState
{
    public UnityEvent<AudioClip> OnWeaponSound;

    private void OnValidate()
    {
        stateType = StateType.AirJumpAttack;
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
        base.LogicUpdate();

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

    protected override void HandleAttackPressed()
    {
    }

    protected override void HandleJumpReleased()
    {
        fsm.Agent.MovementData.SetCurrentVelocity(new Vector2(fsm.Agent.MovementData.CurrentVelocity.x, 0f));

        fsm.TransitionToState(StateType.AirFallAttack);
    }

    private void OnAttackEnd()
    {
        if (fsm.Agent.MovementData.JumpDuration > 0f)
        {
            fsm.Agent.AnimationManager.PlayAnimation(AnimationType.jump);
        }
        else
        {
            fsm.TransitionToState(StateType.Fall);
        }
    }

    private void PerformAttack()
    {
        OnWeaponSound?.Invoke(fsm.Agent.AgentWeapon.GetCurrentWeapon().WeaponSwingSound);
        fsm.Agent.AnimationManager.OnAnimationAction?.RemoveListener(PerformAttack);
        fsm.Agent.AgentWeapon.PerformAttack(fsm.Agent.Data.HittableLayerMask);
    }
}
