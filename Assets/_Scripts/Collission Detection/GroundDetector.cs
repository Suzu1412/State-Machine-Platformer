using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    private Collider2D agentCollider;
    [SerializeField] private CollissionSensesDataSO collissionData;

    private bool isGrounded = false;
    public bool IsGrounded => isGrounded;

    private void Awake()
    {
        if (agentCollider == null)
        {
            agentCollider = GetComponent<Collider2D>();
        }

        if (collissionData == null)
        {
            Debug.LogError(this.name + ": Has no Collission Senses Data attached");
        }
    }

    public void CheckIsGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(agentCollider.bounds.center, agentCollider.bounds.size, 0f, Vector2.down, collissionData.BoxCastYOffset, collissionData.GroundMask);

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
