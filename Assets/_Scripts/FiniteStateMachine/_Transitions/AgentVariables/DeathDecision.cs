using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "State/Decision/Agent/Is Death", fileName = "Decision_IsDeath")]
public class DeathDecision : DecisionSO
{
    internal override bool Decide(Agent agent)
    {
        return agent.HealthSystem.IsDeath;
    }
}
