using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "State/Decision/Collission/Is Touching Ladder", fileName = "Decision_IsTouchingLadder")]

public class TouchingLadderDecision : DecisionSO
{
    internal override bool Decide(Agent agent)
    {
        return agent.CollissionSenses.IsTouchingLadder;
    }
}
