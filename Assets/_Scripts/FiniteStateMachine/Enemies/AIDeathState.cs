using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AIDeathState : State
{
    public UnityEvent OnDeath;

    internal override void EnterState()
    {
        fsm.Agent.AnimationManager.ResetEvents();
        fsm.Agent.AnimationManager.PlayAnimation(AnimationType.death);
        fsm.Agent.AnimationManager.OnAnimationEnd.AddListener(() => OnDeath.Invoke());
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
}
