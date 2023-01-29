using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "State/Decision/Agent/Is Attacking Idle", fileName = "Decision_IsAttackingIdle")]
public class AttackingIdleDecision : DecisionSO
{
    internal override bool Decide(Agent agent)
    {
        return agent.AgentWeapon.IsAttackingIdle;
    }
}
