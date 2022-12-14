using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    private Collider2D agentCollider;
    private CollissionSensesDataSO collissionData;

    [SerializeField] private bool isGrounded = false;
    public bool IsGrounded => isGrounded;

    public void SetCollider(Collider2D agentCollider)
    {
        this.agentCollider = agentCollider;
    }

    public void SetCollissionData(CollissionSensesDataSO data)
    {
        collissionData = data;
    }

    public void CheckIsGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(agentCollider.bounds.center, agentCollider.bounds.size, 0f, Vector2.down, collissionData.BoxCastYOffset, collissionData.GroundMask);

        isGrounded = raycastHit.collider != null ? true : false;
    }

    public void CheckIsGroundedWhileClimbing()
    {
        RaycastHit2D raycastHit = Physics2D.Raycast(agentCollider.bounds.center, transform.TransformDirection(Vector2.down), agentCollider.bounds.extents.y + collissionData.BoxCastYOffset, collissionData.GroundMask);

        isGrounded = raycastHit.collider != null ? true : false;
    }

    #region Gizmos
    private void OnDrawGizmos()
    {
        if (agentCollider == null) return;

        Gizmos.color = collissionData.IsNotCollidingColor;

        if (isGrounded) Gizmos.color = collissionData.IsCollidingColor;

        DrawBottomRightRay(agentCollider);
        DrawBottomLeftRay(agentCollider);
        DrawBottomRay(agentCollider);
    }

    /// <summary>
    /// Draw Ray Gizmos on the right side of the collider in direction to the ground
    /// </summary>
    private void DrawBottomRightRay(Collider2D agentCollider)
    {
        Gizmos.DrawRay(agentCollider.bounds.center + new Vector3(agentCollider.bounds.extents.x, 0), Vector2.down * (agentCollider.bounds.extents.y + collissionData.BoxCastYOffset));
    }

    /// <summary>
    /// Draw Ray Gizmos on the left side of the collider in direction to the ground
    /// </summary>
    private void DrawBottomLeftRay(Collider2D agentCollider)
    {
        Gizmos.DrawRay(agentCollider.bounds.center - new Vector3(agentCollider.bounds.extents.x, 0), Vector2.down * (agentCollider.bounds.extents.y + collissionData.BoxCastYOffset));
    }

    /// <summary>
    /// Draw Ray gizmos that covers all of the ground area of the collider
    /// </summary>
    private void DrawBottomRay(Collider2D agentCollider)
    {
        Gizmos.DrawRay(agentCollider.bounds.center - new Vector3(agentCollider.bounds.extents.x, agentCollider.bounds.extents.y + collissionData.BoxCastYOffset), Vector2.right * (agentCollider.bounds.extents.x * 2));
    }
    #endregion
}
