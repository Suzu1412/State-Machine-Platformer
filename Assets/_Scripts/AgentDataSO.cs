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

    [SerializeField] private float jumpForce = 12f;
    [SerializeField] private float lowJumpMultiplier = 2;
    [SerializeField] private float gravityModifier = 0.5f;

    public float JumpForce => jumpForce;
    public float LowJumpMultiplier => lowJumpMultiplier;
    public float GravityModifier => gravityModifier;
    #endregion
}
