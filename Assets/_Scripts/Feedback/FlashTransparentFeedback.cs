using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashTransparentFeedback : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    [SerializeField] private float feedbackTimeSlow = 0.15f;
    [SerializeField] private float feedbackTimeFast = 0.05f;

    private Coroutine flashAvatarSlowCoroutine;
    private Coroutine flashAvatarFastCoroutine;
    private float invulnerabilityDuration;

    private WaitForSeconds waitForSecondsSlow;
    private WaitForSeconds waitForSecondsFast;

    private WaitForSeconds timeToStartFastCoroutine;
    private WaitForSeconds timeToEndFastCoroutine;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        waitForSecondsSlow = new(feedbackTimeSlow);
        waitForSecondsFast = new(feedbackTimeFast);
    }

    public void Initialize(float invulnerabilityDuration)
    {
        this.invulnerabilityDuration = invulnerabilityDuration;

        timeToStartFastCoroutine = new(this.invulnerabilityDuration - 0.3f);
        timeToEndFastCoroutine = new(this.invulnerabilityDuration);
    }

    public void FeedbackSlowTransparency()
    {
        if (spriteRenderer == null)
        {
            return;
        }

        StopAllCoroutines();

        //flashAvatarSlowCoroutine = StartCoroutine(FlashAvatarSlowCoroutine());
        flashAvatarSlowCoroutine = StartCoroutine(FlashAvatarSlowCoroutine());
    }

    public void FeedbackFastTransparency()
    {
        if (spriteRenderer == null)
        {
            return;
        }

        StopAllCoroutines();

        //flashAvatarSlowCoroutine = StartCoroutine(FlashAvatarSlowCoroutine());
        flashAvatarFastCoroutine = StartCoroutine(FlashAvatarFastCoroutine());
    }

    private void EnableSpriteRenderer(bool value)
    {
        spriteRenderer.enabled = value;
    }

    IEnumerator FlashAvatarSlowCoroutine()
    {
        while (true)
        {
            EnableSpriteRenderer(false);
            yield return waitForSecondsFast;
            EnableSpriteRenderer(true);
            yield return waitForSecondsSlow;

            yield return null;
        }
    }

    IEnumerator FlashAvatarFastCoroutine()
    {
        while (true)
        {
            EnableSpriteRenderer(false);
            yield return waitForSecondsFast;
            EnableSpriteRenderer(true);
            yield return waitForSecondsFast;

            yield return null;
        }
    }

    public void StopFeedback()
    {
        StopAllCoroutines();
        EnableSpriteRenderer(true);
    }
}
