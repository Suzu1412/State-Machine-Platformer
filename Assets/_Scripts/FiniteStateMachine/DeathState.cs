using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathState : State
{
    protected override void EnterState()
    {

    }

    public override void LogicUpdate()
    {
    }

    public override void PhysicsUpdate()
    {
    }

    protected override void ExitState()
    {
        fsm.Agent.Respawn.RespawnFromCheckPoint();
    }
}
