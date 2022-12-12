using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Collission Senses Data", fileName = "Data_")]
public class CollissionSensesDataSO : ScriptableObject
{
    [Header("Mask Parameters:")]
    [Space]
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private LayerMask wallMask;
    [SerializeField] private LayerMask climbingMask;


    [Header("Gizmo Parameters:")]
    [Space]
    [Range(-2f, 2f)]
    [SerializeField] private float boxCastXOffset = 0f;

    [Range(-2f, 2f)]
    [SerializeField] private float boxCastYOffset = -0.1f;

    [Range(0, 2)]
    [SerializeField] private float boxCastWidth = 1;
    [SerializeField] private float boxCastHeight = 1;

    [SerializeField] private Color isCollidingColor = Color.green;
    [SerializeField] private Color isNotCollidingColor = Color.red;


    #region Public Fields
    public LayerMask GroundMask => groundMask;
    public LayerMask WallMask => wallMask;
    public LayerMask ClimbingMask => climbingMask;

    public float BoxCastXOffset => boxCastXOffset;
    public float BoxCastYOffset => boxCastYOffset;
    public float BoxCastWidth => boxCastWidth;
    public float BoxCastHeight => boxCastHeight;
    public Color IsCollidingColor => isCollidingColor;
    public Color IsNotCollidingColor => isNotCollidingColor;
    #endregion
}
