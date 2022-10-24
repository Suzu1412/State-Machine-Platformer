using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Collission Senses Data", fileName = "Data_")]
public class CollissionSensesDataSO : ScriptableObject
{
    [SerializeField] private LayerMask groundMask;



    [Header("Gizmo Parameters:")]
    [Range(-2f, 2f)]
    [SerializeField] private float boxCastXOffset = 0f;

    [Range(-2f, 2f)]
    [SerializeField] private float boxCastYOffset = -0.1f;

    [Range(0, 2)]
    [SerializeField] private float boxCastWidth = 1;
    [SerializeField] private float boxCastHeight = 1;

    [SerializeField] private Color isGroundedColor = Color.green;
    [SerializeField] private Color isNotGroundColor = Color.red;


    #region Public Fields
    public LayerMask GroundMask => groundMask;


    public float BoxCastXOffset => boxCastXOffset;
    public float BoxCastYOffset => boxCastYOffset;
    public float BoxCastWidth => boxCastWidth;
    public float BoxCastHeight => boxCastHeight;
    public Color IsGroundedColor => isGroundedColor;
    public Color IsNotGroundColor => isNotGroundColor;
    #endregion
}
