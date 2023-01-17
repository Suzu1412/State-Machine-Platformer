using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PickableItem : MonoBehaviour
{
    protected SpriteRenderer spriteRenderer;
    private BoxCollider2D pickableCollider;

    [SerializeField] private Color gizmoColor = Color.magenta;

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        pickableCollider= GetComponent<BoxCollider2D>();
    }

    public abstract void PickUp(Agent agent);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (collision.TryGetComponent(out Agent agent))
            {
                PickUp(agent);
                Destroy(gameObject);
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying) 
            return;

        Gizmos.color = gizmoColor;
        Gizmos.DrawCube(pickableCollider.bounds.center, pickableCollider.bounds.size);  
    }
}
