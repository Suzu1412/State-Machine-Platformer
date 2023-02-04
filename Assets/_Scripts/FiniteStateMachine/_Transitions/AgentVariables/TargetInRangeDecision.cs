using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "State/Decision/Agent/Target In Weapon Range", fileName = "Decision_TargetInWeaponRange")]
public class TargetInRangeDecision : DecisionSO
{
    internal override bool Decide(Agent agent)
    {
        return agent.AgentWeapon.IsTargetInRange;
    }
}
