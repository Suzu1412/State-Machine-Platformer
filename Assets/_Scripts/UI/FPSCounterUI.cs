using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FPSCounterUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI fpsCounter;

    private void Update()
    {
        float fps = Mathf.Round(1f / Time.unscaledDeltaTime);
        fpsCounter.text = fps.ToString();
       
    }
}
