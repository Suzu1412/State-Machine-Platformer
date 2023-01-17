using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableWeapon : MonoBehaviour
{
    private Vector2 startPosition = Vector2.zero;
    private RangeWeaponData data;
    private Vector2 movementDirection;
    private bool isInitialized = false;
    private Rigidbody2D rb2d;
    private Transform spriteTransform;
    private float rotationSpeed;

    [Header("Collission Detection Data")]
    [SerializeField] private Vector2 center = Vector2.zero;
    private float radius;
    [SerializeField] private Color gizmoColor = Color.red;
    private LayerMask layerMask;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        if (spriteTransform == null)
        {
            spriteTransform = transform.GetChild(0);
        }
    }

    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        if (isInitialized)
        {
            Fly();
            DetectCollission();

            if (((Vector2) transform.position - startPosition).magnitude >= data.AttackRange)
            {
                Destroy(gameObject);
            }
        }
    }

    private void Fly()
    {
        spriteTransform.rotation *= Quaternion.Euler(0, 0, -movementDirection.x * rotationSpeed * Time.deltaTime);
    }

    private void DetectCollission()
    {
        Collider2D[] collissions = Physics2D.OverlapCircleAll((Vector2) transform.position + center, radius, layerMask);

        if (collissions != null)
        {
            foreach(var collission in collissions)
            {
                if (collission.TryGetComponent(out IHittable hittable))
                {
                    hittable.GetHit(gameObject, data.WeaponDamage);
                    Destroy(gameObject);
                }
                
            }
        }
    }

    public void Initialize(RangeWeaponData data, Vector2 direction, float rotationSpeed, float radius, LayerMask mask)
    {
        this.movementDirection = direction;
        this.data = data;
        rb2d.velocity = movementDirection * data.Speed;
        this.radius = radius;
        this.rotationSpeed = rotationSpeed;
        layerMask = mask;
        isInitialized = true;
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawSphere(transform.position + (Vector3) center, radius);
    }
}
