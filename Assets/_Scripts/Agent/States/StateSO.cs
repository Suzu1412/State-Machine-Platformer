using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateSO : ScriptableObject
{
    public void EnterState(Agent agent)
    {
        agent.Input.OnMovement += HandleMovement;

    }

    public void UpdateState(Agent agent)
    {

    } 

    private void HandleMovement(Vector2 movement)
    {

    }
}
