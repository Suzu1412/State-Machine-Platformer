using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateFactory : MonoBehaviour
{
    [SerializeField] private State idle, move, jump, fall, climb;

    public State GetState(StateType type)
    {
        switch (type)
        {
            case StateType.Idle:
                return idle;
            case StateType.Move:
                return move;
            case StateType.Jump:
                return jump;
            case StateType.Fall:
                return fall;
            case StateType.Climb:
                return climb;
            default:
                Debug.LogError("State Type not defined");
                return idle;
        }
    }

    
}
