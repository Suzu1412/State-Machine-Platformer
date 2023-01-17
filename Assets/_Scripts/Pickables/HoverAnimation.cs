using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverAnimation : MonoBehaviour
{
    [SerializeField] private float movementDistance = 0.5f;
    [SerializeField] private float animationDuration = 1f;
    [SerializeField] private Ease animationEase;

    private void Start()
    {
        transform
            .DOMoveY(transform.position.y + movementDistance, animationDuration)
            .SetEase(animationEase)
            .SetLoops(-1, LoopType.Yoyo);
    }

    private void OnDisable()
    {
        DOTween.Kill(transform);
    }
}
