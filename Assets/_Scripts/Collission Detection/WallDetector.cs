using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallDetector : MonoBehaviour
{
    private Collider2D agentCollider;
    [SerializeField] private CollissionSensesDataSO collissionData;

    private bool isTouchingWall = false;
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

    /// <summary>
    /// Used to Prevent Agent to Keep moving when colliding against Wall
    /// </summary>
    public void CheckIsTouchingWall()
    {
        RaycastHit2D raycastHit = Physics2D.Raycast(agentCollider.bounds.center, transform.TransformDirection(Vector2.right), agentCollider.bounds.extents.x + collissionData.BoxCastXOffset, collissionData.WallMask);

        isTouchingWall = raycastHit.collider != null ? true : false;
    }

    #region Gizmos
    private void OnDrawGizmos()
    {
        if (agentCollider == null) return;

        Gizmos.color = collissionData.IsNotCollidingColor;

        if (isTouchingWall) Gizmos.color = collissionData.IsCollidingColor;

        DrawTouchingWallRay(agentCollider);
    }

    /// <summary>
    /// Draw Ray from center of the transform to the right
    /// </summary>
    private void DrawTouchingWallRay(Collider2D agentCollider)
    {
        Gizmos.DrawRay(agentCollider.bounds.center, transform.TransformDirection(Vector2.right) * agentCollider.bounds.extents.x);
    }
    #endregion
}
