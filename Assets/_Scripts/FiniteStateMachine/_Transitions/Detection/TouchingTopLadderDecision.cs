using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "State/Decision/Collission/Is Touching Top Ladder", fileName = "Decision_IsTouchingTopLadder")]
public class TouchingTopLadderDecision : DecisionSO
{
    internal override bool Decide(Agent agent)
    {
        return agent.CollissionSenses.TopLadder != null;
    }
}
