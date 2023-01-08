using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MeleeAttackDetector : MonoBehaviour
{
    [SerializeField] private bool targetMeleeRangeDetected;
    private CollissionSensesDataSO collissionData;
    public bool TargetMeleeRangeDetected => targetMeleeRangeDetected;

    public UnityEvent<GameObject> OnPlayerDetected;

    [SerializeField] private bool showGizmos;

    private void Awake()
    {
    }

    public void SetCollissionData(CollissionSensesDataSO data)
    {
        collissionData = data;
    }

    public void CheckIfTargetIsInRange()
    {
        targetMeleeRangeDetected = Physics2D.OverlapCircle(transform.position, collissionData.RadiusForGroundAheadDetection, collissionData.TargetMask);

    }

    private void DrawTargetDetectorGizmos(Transform groundCheck)
    {
        Gizmos.DrawWireSphere(groundCheck.position, collissionData.TargetMask);
    }

}
