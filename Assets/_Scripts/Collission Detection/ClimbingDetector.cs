using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbingDetector : MonoBehaviour
{
    private Collider2D agentCollider;
    private CollissionSensesDataSO collissionData;
    [SerializeField] private Collider2D ladder;

    [SerializeField] private bool canClimb = false;
    public bool CanClimb => canClimb;
    public Collider2D Ladder => ladder;
    private Coroutine detectionCoroutine;
    private WaitForSeconds waitForSeconds = new(0.2f);

    private void OnEnable()
    {
        detectionCoroutine = StartCoroutine(DetectionCoroutine());
    }

    private void OnDisable()
    {
        StopCoroutine(detectionCoroutine);
    }

    internal void SetCollider(Collider2D agentCollider)
    {
        this.agentCollider = agentCollider;
    }

    internal void SetCollissionData(CollissionSensesDataSO data)
    {
        collissionData = data;

        waitForSeconds = new(collissionData.LadderDetectionDelay);
    }

    private IEnumerator DetectionCoroutine()
    {
        while (true)
        {
            if (collissionData == null)
            {
                yield return null;
            }

            yield return waitForSeconds;

            CheckIfCanClimb();
        }
    }

    private void CheckIfCanClimb()
    {
        RaycastHit2D raycastHit = Physics2D.Raycast(agentCollider.bounds.center, transform.TransformDirection(Vector2.down), agentCollider.bounds.extents.y + collissionData.BoxCastYOffset, collissionData.ClimbingMask);

        ladder = raycastHit.collider != null ? raycastHit.collider : null;

        canClimb = raycastHit.collider != null;
    }

    #region Gizmos
    private void OnDrawGizmosSelected()
    {
        if (agentCollider == null) return;

        Gizmos.color = collissionData.IsNotCollidingColor;

        if (canClimb) Gizmos.color = collissionData.IsCollidingColor;

        DrawTouchingLadderRay(agentCollider);
    }

    /// <summary>
    /// Draw Ray from center of the transform to the right
    /// </summary>
    private void DrawTouchingLadderRay(Collider2D agentCollider)
    {
        Gizmos.DrawRay(agentCollider.bounds.center + (Vector3.up * (agentCollider.bounds.extents.y + collissionData.BoxCastYOffset)), transform.TransformDirection(Vector2.down) * (agentCollider.bounds.extents.y + collissionData.BoxCastYOffset) * 2);
    }
    #endregion
}
