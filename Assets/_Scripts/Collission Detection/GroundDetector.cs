using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    private Collider2D agentCollider;
    private CollissionSensesDataSO collissionData;
    private Vector2 originWhileClimbing;
    [SerializeField] private bool isClimbing;
    [SerializeField] private bool isGrounded = false;
    private Coroutine detectionCoroutine;
    private WaitForSeconds waitForSeconds = new(0.2f);

    public bool IsGrounded => isGrounded;
    public bool IsClimbing { 
        get => isClimbing; 
        set { 
            isClimbing = value;

            if (IsClimbing)
            {
                isGrounded = false;
            }
        } 
    }

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

        waitForSeconds = new(collissionData.GroundDetectionDelay);
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

            if (!isClimbing)
            {
                CheckIsGrounded();
            }
            else
            {
                CheckIsGroundedWhileClimbing();
            }
        }
    }

    private void CheckIsGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(agentCollider.bounds.center, agentCollider.bounds.size, 0f, Vector2.down, collissionData.BoxCastYOffset, collissionData.GroundMask);

        isGrounded = raycastHit.collider != null;
    }

    private void CheckIsGroundedWhileClimbing()
    {
        originWhileClimbing.Set(agentCollider.bounds.center.x, agentCollider.bounds.center.y);

        RaycastHit2D raycastHit = Physics2D.Raycast(originWhileClimbing, transform.TransformDirection(Vector2.down), agentCollider.bounds.extents.y + collissionData.BoxCastYOffset, collissionData.GroundMask);

        isGrounded = raycastHit.collider != null;
    }

    #region Gizmos
    private void OnDrawGizmosSelected()
    {
        if (agentCollider == null) return;

        Gizmos.color = collissionData.IsNotCollidingColor;

        if (isGrounded) Gizmos.color = collissionData.IsCollidingColor;

        if (!IsClimbing)
        {
            DrawBottomRightRay(agentCollider);
            DrawBottomLeftRay(agentCollider);
            DrawBottomRay(agentCollider);
        }
        else
        {
            DrawBottomRayWhileClimbing(agentCollider);
        }
        
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

    private void DrawBottomRayWhileClimbing(Collider2D agentCollider)
    {
        Gizmos.DrawRay(originWhileClimbing, transform.TransformDirection(Vector2.down) * (collissionData.BoxCastYOffset * 2));
    }
    #endregion
}
