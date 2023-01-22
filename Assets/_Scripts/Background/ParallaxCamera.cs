using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[ExecuteInEditMode]
public class ParallaxCamera : MonoBehaviour
{
    //public delegate void ParallaxCameraDelegate();
    //public ParallaxCameraDelegate onCameraTranslate;
    public Action<float> OnParallaxCamera;
    private float oldPosition;
    private float currentPosition;

    void Start()
    {
        oldPosition = transform.position.x;
    }
    void FixedUpdate()
    {
        oldPosition = Mathf.Round(oldPosition * 10.0f) * 0.1f;
        currentPosition = Mathf.Round(transform.position.x * 10.0f) * 0.1f;

        if (currentPosition != oldPosition)
        {
            float delta = oldPosition - currentPosition;
            OnParallaxCamera?.Invoke(delta);
            oldPosition = transform.position.x;
        }
    }


    //public delegate void ParallaxCameraDelegate(float deltaMovement);
    //public ParallaxCameraDelegate onCameraTranslate;
    //private float oldPosition;
    //private float currentPosition;

    //void Start()
    //{
    //    oldPosition = transform.position.x;
    //}
    //void Update()
    //{
    //    oldPosition = Mathf.Round(oldPosition * 10.0f) * 0.1f;
    //    currentPosition = Mathf.Round(transform.position.x * 10.0f) * 0.1f;

    //    if (currentPosition != oldPosition)
    //    {
    //        if (onCameraTranslate != null)
    //        {
    //            float delta = oldPosition - currentPosition;
    //            onCameraTranslate(delta);
    //        }
    //        oldPosition = transform.position.x;
    //    }
    //}
}