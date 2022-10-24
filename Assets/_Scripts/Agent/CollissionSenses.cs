using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollissionSenses : MonoBehaviour
{
    private Collider2D agentCollider;
    [SerializeField] private CollissionSensesDataSO collissionData;

    private bool isGrounded = false;
    private bool isTouchingWall = false;
    public bool IsGrounded => isGrounded;
    public bool IsTouchingWall => isTouchingWall;


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
        RaycastHit2D hit = Physics2D.BoxCast(agentCollider.bounds.center + 
            new Vector3(collissionData.BoxCastXOffset,
            collissionData.BoxCastYOffset, 0f), 
            new Vector2(collissionData.BoxCastWidth, collissionData.BoxCastHeight), 0, 
            Vector2.down, collissionData.GroundMask);

        isGrounded = hit.collider != null ? true : false;
    }

    /// <summary>
    /// Used to Prevent Agent to Keep moving when colliding against Wall
    /// </summary>
    public void CheckIsTouchingWall()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.right), collissionData.BoxCastWidth, collissionData.GroundMask);

        isTouchingWall = hit.collider != null ? true : false;
    }


    #region Gizmos
    private void OnDrawGizmos()
    {
        if (agentCollider == null) return;

        Gizmos.color = collissionData.IsNotGroundColor;

        if (isGrounded) Gizmos.color = collissionData.IsGroundedColor;

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
