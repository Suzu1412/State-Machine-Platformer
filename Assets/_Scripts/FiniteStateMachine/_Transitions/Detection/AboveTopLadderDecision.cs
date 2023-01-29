using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "State/Decision/Collission/Is Above Top Ladder", fileName = "Decision_IsAboveTopLadder")]
public class AboveTopLadderDecision : DecisionSO
{
    internal override bool Decide(Agent agent)
    {
        return agent.CollissionSenses.IsAboveOfTopLadder;
    }
}
