using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateFactory : MonoBehaviour
{
    //[SerializeField] private State idle, move, jump, fall, climb, roll, attack, movingAttack, airJumpAttack, airFallAttack, hit, death;
    [SerializeField] private State[] states;

    public State GetState(StateType type)
    {
        for (int i=0; i < states.Length; i++)
        {
            if (states[i].StateType == type)
            {
                return states[i];
            }
        }

        Debug.LogError("State Type " + type + " not defined in: " + this.gameObject.name);
        return null;
    }

    
}
