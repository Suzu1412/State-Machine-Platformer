using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class BackgroundParallaxEffect : MonoBehaviour
{
    private Camera mainCamera;
    private CinemachineVirtualCamera cm;
    [Tooltip("The velocity in which the background will move. The objects further away should move slower")]
    [SerializeField] private Vector2 scrollSpeed;
    private Rigidbody2D targetRB;

    private Vector2 offset;

    private Material material;

    private float oldPosition;
    private float currentPosition;

    private void Awake()
    {
        material = GetComponent<SpriteRenderer>().material;
        cm = transform.root.GetComponentInChildren<CinemachineVirtualCamera>();
        
        if (cm == null)
        {
            Debug.LogError(this.name + " has not found Cinemachine Virtual Camera. Make sure its " +
                "in the correct order");
            return;
        }

        if (cm.Follow == null)
        {
            Debug.LogError(this.name + " has not found a Follow Target in CM Virtual Camera. " +
                "Make sure to assign one first");
            return;
        }

        

        if (cm.Follow.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb2d))
        {
            targetRB = rb2d;
        }
        else
        {
            Debug.LogError(this.name + " has not found a Rigidbody2D in the Follow Target");
            return;
        }
    }

    void Start()
    {
        mainCamera = Camera.main;
        oldPosition = mainCamera.transform.position.x;
    }

    private void FixedUpdate()
    {
        oldPosition = Mathf.Round(oldPosition * 10.0f) * 0.1f;
        currentPosition = Mathf.Round(mainCamera.transform.position.x * 10.0f) * 0.1f;

        if (currentPosition != oldPosition)
        {
            offset = (targetRB.velocity.x * 0.1f) * scrollSpeed * Time.deltaTime;
            material.mainTextureOffset += offset;

            oldPosition = mainCamera.transform.position.x;
        }
    }
}
