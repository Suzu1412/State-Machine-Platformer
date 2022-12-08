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

    }

    #region Gizmos
    private void OnDrawGizmos()
    {
        if (agentCollider == null) return;

        Gizmos.color = collissionData.IsNotCollidingColor;

        if (isOnTop) Gizmos.color = collissionData.IsCollidingColor;

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
