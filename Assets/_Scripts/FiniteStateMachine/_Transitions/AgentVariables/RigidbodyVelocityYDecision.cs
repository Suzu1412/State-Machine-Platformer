using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "State/Decision/Agent/Rigidbody Velocity Y", fileName = "Decision_RigidbodyVelocityY_")]
public class RigidbodyVelocityYDecision : DecisionValueComparerSO
{
    internal override bool Decide(Agent agent)
    {
        return ValueCompare(agent.Rb2d.velocity.y);
    }
}
