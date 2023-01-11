using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathFlashFeedback : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    [SerializeField] private float feedbackTime = 0.1f;
    [SerializeField] private float slowFeedbackTime = 0.2f;

    private Coroutine deathFlashCoroutine;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Feedback()
    {
        if (spriteRenderer == null)
        {
            return;
        }

        StopAllCoroutines();

        deathFlashCoroutine = StartCoroutine(DeathFlash());
    }

    private void EnableSpriteRenderer(bool value)
    {
        spriteRenderer.enabled = value;
    }

    IEnumerator DeathFlash()
    {
        for (int i = 0; i < 2; i++)
        {
            EnableSpriteRenderer(false);
            yield return new WaitForSeconds(feedbackTime);
            EnableSpriteRenderer(true);
            yield return new WaitForSeconds(slowFeedbackTime);
        }

        for (int i = 0; i < 2; i++)
        {
            EnableSpriteRenderer(false);
            yield return new WaitForSeconds(feedbackTime);
            EnableSpriteRenderer(true);
            yield return new WaitForSeconds(feedbackTime);
        }

        transform.parent.gameObject.SetActive(false);
    }
}
