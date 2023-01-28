using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DeathState : State
{
    public UnityEvent OnDeathAnimation;

    internal override void EnterState()
    {
        fsm.Agent.AnimationManager.ResetEvents();
        fsm.Agent.AnimationManager.PlayAnimation(AnimationType.death);
        fsm.Agent.AnimationManager.OnAnimationEnd.AddListener(() => DeathBehaviour());
    }

    internal override void LogicUpdate()
    {
    }

    internal override void PhysicsUpdate()
    {
        fsm.Agent.Rb2d.velocity = new Vector2(0, fsm.Agent.Rb2d.velocity.y);
    }

    internal override void ExitState()
    {
        if (fsm.Agent.Respawn != null)
        {
            fsm.Agent.gameObject.SetActive(true);
            fsm.Agent.Respawn.RespawnFromCheckPoint();
        }
          
    }

    private void DeathBehaviour()
    {
        OnDeathAnimation?.Invoke();
        //fsm.Agent.gameObject.SetActive(false);
    }

    protected override void HandleMovement(Vector2 input)
    {
    }

    protected override void HandleFaceDirection(Vector2 input)
    {
    }

    protected override void HandleJumpPressed()
    {
    }

    protected override void HandleJumpReleased()
    {
    }

    protected override void HandleAttackPressed()
    {
    }

    protected override void HandleRollPressed()
    {
    }

    protected override void HandleRollReleased()
    {
    }

}
