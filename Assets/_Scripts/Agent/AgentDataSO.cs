using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Agent Data", fileName = "Data_")]
public class AgentDataSO : ScriptableObject
{
    #region Movement Data
    [Header("Movement Data")]
    [Space]

    [SerializeField] private float maxSpeed = 6f;
    [SerializeField] private float acceleration = 50f;
    [SerializeField] private float deacceleration = 50f;

    public float MaxSpeed => maxSpeed;
    public float Acceleration => acceleration;
    public float Deacceleration => deacceleration;


    #endregion

    #region Jump Data
    [Header("Jump Data")]
    [Space]
    [SerializeField] private float jumpSpeed;
    [Range(1, 5)] [SerializeField] private int amountOfJumps = 1;
    [Range(0.1f, 0.5f)] [SerializeField] private float jumpDuration = 0.25f;
    [Range(-30f, -10f)] [SerializeField] private float maxFallSpeed;
    [SerializeField] private float coyoteDuration;

    public float JumpSpeed => jumpSpeed;
    public int AmountOfJumps => amountOfJumps;
    public float JumpDuration => jumpDuration;
    public float MaxFallSpeed => maxFallSpeed;
    public float CoyoteDuration => coyoteDuration;
    #endregion
}
