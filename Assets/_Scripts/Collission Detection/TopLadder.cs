using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopLadder : MonoBehaviour
{
    /*
    [SerializeField] private LayerMask agentMask;
    private Collider2D topCollider;
    private Color isCollidingColor = Color.green;
    private Color isNotCollidingColor = Color.red;
    private bool isOnTop, isOnBottom;

    private void Awake()
    {
        topCollider = GetComponentInChildren<Collider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (topCollider.IsTouchingLayers(agentMask))
        {
            collision.gameObject.GetComponentInChildren<ClimbingDetector>().SetTopLadder(topCollider);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (!topCollider.IsTouchingLayers(agentMask))
        {
            collision.gameObject.GetComponentInChildren<ClimbingDetector>().SetTopLadder(null);
        }
    }

    private void Update()
    {
        CheckIfPlayerIsOnTop();
    }

    private void CheckIfPlayerIsOnTop()
    {
        RaycastHit2D raycastHit = Physics2D.Raycast(topCollider.bounds.center, transform.TransformDirection(Vector2.up), topCollider.bounds.extents.y + 0.1f, agentMask);
    
        if (raycastHit.collider != null)
        {
            raycastHit.collider.GetComponent<ClimbingDetector>().SetTopLadder(topCollider);
            isOnTop = true;
        }
        else
        {
            isOnTop = false;
        }

      
    }

    private void OnDrawGizmos()
    {
        if (topCollider == null) return;

        Gizmos.color = isNotCollidingColor;

        if (isOnTop) Gizmos.color = isCollidingColor;

        DrawRayOnTopLadder(topCollider);
    }

    private void DrawRayOnTopLadder(Collider2D topCollider)
    {
        Vector3 direction = transform.TransformDirection(Vector2.up) * (topCollider.bounds.extents.y + 0.1f);
        Gizmos.DrawRay(topCollider.bounds.center, direction);
    }

    private void OnBecameVisible()
    {
        this.gameObject.SetActive(true);
    }

    private void OnBecameInvisible()
    {
        this.gameObject.SetActive(false);
    }
    */
}
