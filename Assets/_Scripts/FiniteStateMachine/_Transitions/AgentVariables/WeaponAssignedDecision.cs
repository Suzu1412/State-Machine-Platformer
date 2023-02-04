using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "State/Decision/Agent/Weapon Assigned", fileName = "Decision_WeaponAssigned")]
public class WeaponAssignedDecision : DecisionSO
{
    internal override bool Decide(Agent agent)
    {
        return agent.AgentWeapon.GetCurrentWeapon() != null;
    }
}
