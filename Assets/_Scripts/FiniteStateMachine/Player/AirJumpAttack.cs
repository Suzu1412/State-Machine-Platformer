using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AirJumpAttack : JumpState
{
    public UnityEvent<AudioClip> OnWeaponSound;
    private Vector2 direction;
    private bool showGizmos;

    protected override void EnterState()
    {
        fsm.Agent.AnimationManager.ResetEvents();
        fsm.Agent.AnimationManager.PlayAnimation(AnimationType.attack);
        fsm.Agent.AnimationManager.OnAnimationAction.AddListener(() => PerformAttack());
        fsm.Agent.AnimationManager.OnAnimationEnd.AddListener(() => OnAttackEnd());

        fsm.Agent.AgentWeapon.ToggleWeaponVisibility(true);
        direction = fsm.Agent.transform.right * (fsm.Agent.transform.localScale.x > 0 ? 1 : -1);
        showGizmos = true;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    protected override void ExitState()
    {
        base.ExitState();
        fsm.Agent.AnimationManager.ResetEvents();
        showGizmos = false;
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
        fsm.Agent.AgentWeapon.GetCurrentWeapon().PerformAttack(fsm.Agent.AgentWeapon.transform, fsm.Agent.HittableLayerMask, direction);
    }

    private void OnDrawGizmos()
    {
        if (Application.isPlaying == false) return;

        if (!showGizmos) return;

        fsm.Agent.AgentWeapon.GetCurrentWeapon().DrawWeaponGizmos(fsm.Agent.AgentWeapon.transform.position, direction);
    }
}
