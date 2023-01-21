using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointPickable : PickableItem
{
    [SerializeField] private int amount = 1;

    public override void PickUp(Agent agent)
    {
        if (agent == null)
        {
            Debug.LogError("porque el agente esta vacio");
        }

        if (agent.TryGetComponent(out PlayerPoints points))
        {
            points.Add(amount);
        }

    }
}
