using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateFactory : MonoBehaviour
{
    [SerializeField] private State idle, move, jump, fall, climb, roll, attack, movingAttack, airJumpAttack, airFallAttack, hit, death;

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
            case StateType.Roll:
                return roll;
            case StateType.Attack:
                return attack;
            case StateType.MovingAttack:
                return movingAttack;
            case StateType.AirJumpAttack:
                return airJumpAttack;
            case StateType.AirFallAttack:
                return airFallAttack;
            case StateType.Hit:
                return hit;
            case StateType.Death:
                return death;
            default:
                Debug.LogError("State Type not defined");
                return idle;
        }
    }

    
}
