using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopLadderDetector : MonoBehaviour
{
    private Collider2D agentCollider;
    private CollissionSensesDataSO collissionData;
    [SerializeField] private Collider2D topLadder;

    [SerializeField] private bool isBelowLadder = false;
    [SerializeField] private bool isAboveLadder = false;
    private Vector2 originBottom;
    private Vector2 originTop;
    private Vector2 origin;
    private Coroutine detectionCoroutine;
    private WaitForSeconds waitForSeconds = new(0.2f);


    public Collider2D TopLadder => topLadder;

    public bool IsAboveLadder => isAboveLadder;
    public bool IsBelowLadder => isBelowLadder;

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

        waitForSeconds = new(collissionData.TopLadderDetectionDelay);
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

            DetectTopLadder();
            CheckIfLadderAboveAgent();
            CheckIfLadderBelowAgent();
        }
    }

    private void DetectTopLadder()
    {
        origin.Set(agentCollider.bounds.center.x, agentCollider.bounds.max.y);

        RaycastHit2D raycastHit = Physics2D.Raycast(origin, transform.TransformDirection(Vector2.down), agentCollider.bounds.extents.y * 2 + collissionData.BoxCastYOffset, collissionData.GroundMask);

        topLadder = null;

        if (raycastHit.collider != null)
        {
            if (raycastHit.collider.TryGetComponent(out TopLadder _))
            {
                topLadder = raycastHit.collider;
            }
        }

    }

    private void CheckIfLadderAboveAgent()
    {
        originBottom.Set(agentCollider.bounds.center.x, agentCollider.bounds.min.y);

        RaycastHit2D raycastHit = Physics2D.Raycast(originBottom, transform.TransformDirection(Vector2.down), collissionData.BoxCastYOffset * 2, collissionData.GroundMask);

        isAboveLadder = false;

        if (raycastHit.collider != null)
        {
            if (raycastHit.collider.TryGetComponent(out TopLadder _))
            {
                isAboveLadder = true;
            }
        }
    }

    private void CheckIfLadderBelowAgent()
    {
        originTop.Set(agentCollider.bounds.center.x, agentCollider.bounds.center.y);

        RaycastHit2D raycastHit = Physics2D.Raycast(originTop, transform.TransformDirection(Vector2.up), agentCollider.bounds.extents.y + collissionData.BoxCastYOffset, collissionData.GroundMask);

        isBelowLadder = false;

        if (raycastHit.collider != null)
        {
            if (raycastHit.collider.TryGetComponent(out TopLadder _))
            {
                isBelowLadder = true;
            }
        }
    }

    #region Gizmos
    private void OnDrawGizmosSelected()
    {
        if (agentCollider == null) return;

        Gizmos.color = collissionData.IsNotCollidingColor;

        if (isBelowLadder) Gizmos.color = collissionData.IsCollidingColor;

        DrawLadderBelowRay(agentCollider);
        DrawLadderAboveRay(agentCollider);
    }

    /// <summary>
    /// Draw Ray from center of the transform to the right
    /// </summary>
    private void DrawLadderBelowRay(Collider2D agentCollider)
    {
        Gizmos.DrawRay(agentCollider.bounds.center + (Vector3.up * (agentCollider.bounds.extents.y + collissionData.BoxCastYOffset)), (agentCollider.bounds.extents.y + collissionData.BoxCastYOffset) * 2 * transform.TransformDirection(Vector2.down));
    }

    private void DrawLadderAboveRay(Collider2D agentCollider)
    {
        Gizmos.DrawRay(originBottom, transform.TransformDirection(Vector2.down) * collissionData.BoxCastYOffset * 2);
    }
    #endregion
}
