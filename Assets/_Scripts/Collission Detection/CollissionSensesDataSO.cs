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
    [SerializeField] private LayerMask targetMask;

    [Header("Detection Delay Parameters:")]
    [Space]
    [SerializeField] [Range(0.05f, 0.3f)] private float groundDetectionDelay = 0.05f;
    [SerializeField] [Range(0.05f, 0.3f)] private float groundAheadDetectionDelay = 0.3f;
    [SerializeField] [Range(0.05f, 0.3f)] private float wallDetectionDelay = 0.2f;
    [SerializeField] [Range(0.05f, 0.3f)] private float ladderDetectionDelay = 0.3f;
    [SerializeField] [Range(0.05f, 0.3f)] private float topLadderDetectionDelay = 0.3f;
    [SerializeField] [Range(0.05f, 0.3f)] private float targetDetectionDelay = 0.3f;


    [Header("Gizmo Parameters:")]
    [Space]
    [Range(0.1f, 1f)]
    [Tooltip("Used Only for AI Patrolling")]
    [SerializeField] private float radiusForGroundAheadDetection = 0.2f;

    [Range(-2f, 2f)]
    [SerializeField] private float boxCastXOffset = 0f;

    [Range(-2f, 2f)]
    [SerializeField] private float boxCastYOffset = -0.1f;

    [Range(0, 2)]
    [SerializeField] private float boxCastWidth = 1;
    [SerializeField] private float boxCastHeight = 1;

    [Header("Detector for AI")]
    [SerializeField] private Vector2 targetDetectorSize = Vector2.one;
    [SerializeField] private Vector2 detectorOriginOffset = Vector2.zero;

    [SerializeField] private Color isCollidingColor = Color.green;
    [SerializeField] private Color isNotCollidingColor = Color.red;


    #region Public Fields
    public LayerMask GroundMask => groundMask;
    public LayerMask WallMask => wallMask;
    public LayerMask ClimbingMask => climbingMask;
    public LayerMask TargetMask => targetMask;

    public float GroundDetectionDelay => groundDetectionDelay;
    public float GroundAheadDetectionDelay => groundAheadDetectionDelay;
    public float WallDetectionDelay => wallDetectionDelay;
    public float LadderDetectionDelay => ladderDetectionDelay;
    public float TopLadderDetectionDelay => topLadderDetectionDelay;
    public float TargetDetectionDelay => targetDetectionDelay;

    public float RadiusForGroundAheadDetection => radiusForGroundAheadDetection;
    public float BoxCastXOffset => boxCastXOffset;
    public float BoxCastYOffset => boxCastYOffset;
    public float BoxCastWidth => boxCastWidth;
    public float BoxCastHeight => boxCastHeight;
    public Vector2 TargetDetectorSize => targetDetectorSize;
    public Vector2 DetectorOriginOffset => detectorOriginOffset;
    public Color IsCollidingColor => isCollidingColor;
    public Color IsNotCollidingColor => isNotCollidingColor;
    #endregion
}
