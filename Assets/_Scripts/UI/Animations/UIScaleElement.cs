using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScaleElement : MonoBehaviour
{
    private Sequence sequence;
    [SerializeField] private RectTransform element;
    [SerializeField] private float animationEndScale;
    [SerializeField] private float animationTime;
    [SerializeField] private bool playConstantly = false;
    private readonly float baseScaleValue;
    private Vector2 baseScale;
    private Vector2 endScale;


    // Start is called before the first frame update
    void Start()
    {
        baseScale = element.localScale;
        endScale = Vector3.one * animationEndScale;

        if (playConstantly)
        {

            Debug.Log("esta funcionando");
            sequence = DOTween.Sequence().
                Append(element.DOScale(baseScale, animationTime)).
                Append(element.DOScale(endScale, animationTime));

            sequence.SetLoops(-1, LoopType.Restart);
            sequence.Play();
        }
    }

    public void PlayAnimation()
    {
        sequence = DOTween.Sequence().
                Append(element.DOScale(baseScale, animationTime)).
                Append(element.DOScale(endScale, animationTime));

        sequence.Play();
    }

    private void OnDestroy()
    {
        if (sequence != null)
        {
            sequence.Kill();
        }
    }
}
