using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbingDetector : MonoBehaviour
{
    private Collider2D agentCollider;
    private CollissionSensesDataSO collissionData;
    [SerializeField] private Collider2D ladder;
    [SerializeField] private Collider2D topLadder;

    [SerializeField] private bool canClimb = false;
    public bool CanClimb => canClimb;
    public Collider2D Ladder => ladder;
    public Collider2D TopLadder => topLadder;

    private void Awake()
    {
        if (agentCollider == null)
        {
            agentCollider = GetComponent<Collider2D>();
        }
    }

    public void SetCollissionData(CollissionSensesDataSO data)
    {
        collissionData = data;
    }

    public void CheckIfCanClimb()
    {
        RaycastHit2D raycastHit = Physics2D.Raycast(agentCollider.bounds.center, transform.TransformDirection(Vector2.down), agentCollider.bounds.extents.y + collissionData.BoxCastYOffset, collissionData.ClimbingMask);

        if (raycastHit.collider != null) ladder = raycastHit.collider.GetComponent<BoxCollider2D>();

        canClimb = raycastHit.collider != null ? true : false;
    }

    #region Gizmos
    private void OnDrawGizmos()
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

    public void SetTopLadder(Collider2D topLadder)
    {
        this.topLadder = topLadder;
    }
}
