using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "State/Decision/Agent/Is Hit", fileName = "Decision_IsHit")]
public class HitDecision : DecisionSO
{
    internal override bool Decide(Agent agent)
    {
        return agent.HealthSystem.IsHit;
    }
}
