using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopLadderDetector : MonoBehaviour
{
    private Collider2D agentCollider;
    private CollissionSensesDataSO collissionData;
    [SerializeField] private Collider2D topLadder;

    [SerializeField] private bool isOnTop = false;
    [SerializeField] private bool isOnBottom = false;
    private Vector2 origin;

    public Collider2D TopLadder => topLadder;

    public bool IsOnBottom => isOnBottom;

    public void SetCollider(Collider2D agentCollider)
    {
        this.agentCollider = agentCollider;
    }

    public void SetCollissionData(CollissionSensesDataSO data)
    {
        collissionData = data;
    }

    public void CheckIfOnTop()
    {
        RaycastHit2D raycastHit = Physics2D.Raycast(agentCollider.bounds.center, transform.TransformDirection(Vector2.down), agentCollider.bounds.extents.y + collissionData.BoxCastYOffset, collissionData.GroundMask);

        topLadder = null;
        isOnTop = false;

        if (raycastHit.collider != null)
        {
            if (raycastHit.collider.GetComponent<TopLadder>() != null)
            {
                topLadder = raycastHit.collider;
                isOnTop = true;
            }
        }
    }

    public void CheckIfOnBottom()
    {
        origin.Set(agentCollider.bounds.center.x, agentCollider.bounds.min.y);

        RaycastHit2D raycastHit = Physics2D.Raycast(origin, transform.TransformDirection(Vector2.down), collissionData.BoxCastYOffset, collissionData.GroundMask);

        isOnBottom = false;

        if (raycastHit.collider != null)
        {
            if (raycastHit.collider.GetComponent<TopLadder>() != null)
            {
                isOnBottom = true;
            }
        }
    }

    #region Gizmos
    private void OnDrawGizmos()
    {
        if (agentCollider == null) return;

        Gizmos.color = collissionData.IsNotCollidingColor;

        if (isOnTop) Gizmos.color = collissionData.IsCollidingColor;

        DrawLadderRayOnTop(agentCollider);
        DrawLadderRayOnBottom(agentCollider);
    }

    /// <summary>
    /// Draw Ray from center of the transform to the right
    /// </summary>
    private void DrawLadderRayOnTop(Collider2D agentCollider)
    {
        Gizmos.DrawRay(agentCollider.bounds.center + (Vector3.up * (agentCollider.bounds.extents.y + collissionData.BoxCastYOffset)), transform.TransformDirection(Vector2.down) * (agentCollider.bounds.extents.y + collissionData.BoxCastYOffset) * 2);
    }

    private void DrawLadderRayOnBottom(Collider2D agentCollider)
    {
        Gizmos.DrawRay(new Vector3(agentCollider.bounds.center.x, agentCollider.bounds.min.y, agentCollider.bounds.center.z) , transform.TransformDirection(Vector2.down) * collissionData.BoxCastYOffset);
    }
    #endregion
}
