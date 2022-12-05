using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopLadder : MonoBehaviour
{
    [SerializeField] private LayerMask agentMask;
    private Collider2D topCollider;

    private void Awake()
    {
        topCollider = GetComponent<Collider2D>();
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
}
