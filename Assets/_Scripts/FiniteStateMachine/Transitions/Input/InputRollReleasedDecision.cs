using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "State/Decision/Input/Roll Input Released", fileName = "Decision_InputRollReleased")]

public class InputRollReleasedDecision : DecisionSO
{
    internal override bool Decide(Agent agent)
    {
        return agent.Input.RollReleased;
    }
}
