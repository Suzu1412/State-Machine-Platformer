using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundAheadDetector : MonoBehaviour
{
    [SerializeField] private Transform groundCheck;
    private CollissionSensesDataSO collissionData;

    [SerializeField] private bool isThereGroundAhead = false;
    public bool IsThereGroundAhead => isThereGroundAhead;

    private void Awake()
    {
        if (groundCheck == null) Debug.LogError("There is no Ground Check Transform assigned to: " + this.name);
    }

    public void SetCollissionData(CollissionSensesDataSO data)
    {
        collissionData = data;
    }

    /// <summary>
    /// Used to Prevent Agent to Keep moving when the path end
    /// </summary>
    public void CheckIfThereIsPathAhead()
    {
        isThereGroundAhead = Physics2D.OverlapCircle(groundCheck.position, collissionData.RadiusForGroundAheadDetection, collissionData.GroundMask);
    }

    private void OnDrawGizmos()
    {
        if (groundCheck == null) return;
        if (collissionData == null) return;

        Gizmos.color = collissionData.IsNotCollidingColor;

        if (isThereGroundAhead) Gizmos.color = collissionData.IsCollidingColor;

        DrawPathDetectorGizmos(groundCheck);
    }

    /// <summary>
    /// Draw Ray from center of the transform to the right
    /// </summary>
    private void DrawPathDetectorGizmos(Transform groundCheck)
    {
        Gizmos.DrawWireSphere(groundCheck.position, collissionData.RadiusForGroundAheadDetection);
    }
}
