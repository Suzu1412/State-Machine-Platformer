using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "State/Decision/Input/Input Y", fileName = "Decision_InputY_")]
public class InputDirectionYDecision : DecisionValueComparerSO
{
    internal override bool Decide(Agent agent)
    {
        return ValueCompare(agent.Input.MovementVector.y);
    }
}
