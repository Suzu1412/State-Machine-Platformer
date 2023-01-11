using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashTransparentFeedback : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    [SerializeField] private float feedbackTime = 0.1f;

    private Coroutine flashAvatarCoroutine;

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

        flashAvatarCoroutine = StartCoroutine(FlashAvatar());
    }

    private void EnableSpriteRenderer(bool value)
    {
        spriteRenderer.enabled = value;
    }

    IEnumerator FlashAvatar()
    {
        for (int i=0; i < 3; i++)
        {
            EnableSpriteRenderer(false);
            yield return new WaitForSeconds(feedbackTime);
            EnableSpriteRenderer(true);
            yield return new WaitForSeconds(feedbackTime);
        }
    }
}
