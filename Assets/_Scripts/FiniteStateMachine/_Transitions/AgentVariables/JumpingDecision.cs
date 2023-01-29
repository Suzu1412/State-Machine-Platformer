using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "State/Decision/Agent/Is Jumping", fileName = "Decision_IsJumping")]
public class JumpingDecision : DecisionSO
{
    internal override bool Decide(Agent agent)
    {
        return agent.MovementData.IsJumping;
    }
}
