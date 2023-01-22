using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    private Camera mainCamera;

    [Range(0f, 1f)] private float movementSpeed = 0.3f;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void FixedUpdate()
    {
        transform.position = new Vector2(mainCamera.transform.position.x * movementSpeed, 0);
    }
}
