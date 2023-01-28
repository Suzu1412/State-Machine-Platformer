using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "State/Decision/Input/Jump Input Released", fileName = "Decision_InputJumpReleased")]

public class InputJumpReleasedDecision : DecisionSO
{
    internal override bool Decide(Agent agent)
    {
        return agent.Input.JumpReleased;
    }
}
