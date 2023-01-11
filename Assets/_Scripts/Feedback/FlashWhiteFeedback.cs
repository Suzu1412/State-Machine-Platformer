using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashWhiteFeedback : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    [SerializeField] private float feedbackTime = 0.1f;

    private Coroutine resetColorCoroutine;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Feedback()
    {
        if (spriteRenderer == null || !spriteRenderer.material.HasProperty("_MakeSolidColor"))
        {
            return;
        }

        ToggleMaterial(1);
        StopAllCoroutines();

        resetColorCoroutine = StartCoroutine(ResetColor());
    }

    private void ToggleMaterial(int value)
    {
        value = Mathf.Clamp(value, 0, 1);
        spriteRenderer.material.SetInt("_MakeSolidColor", value); 
    }

    IEnumerator ResetColor()
    {
        yield return new WaitForSeconds(feedbackTime);
        ToggleMaterial(0);
    }
}

