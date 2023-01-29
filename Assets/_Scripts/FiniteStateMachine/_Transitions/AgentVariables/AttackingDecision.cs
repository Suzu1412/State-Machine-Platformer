using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "State/Decision/Agent/Is Attacking", fileName = "Decision_IsAttacking")]
public class AttackingDecision : DecisionSO
{
    internal override bool Decide(Agent agent)
    {
        return agent.AgentWeapon.IsAttacking;
    }
}
