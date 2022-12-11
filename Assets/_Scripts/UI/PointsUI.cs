using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class PointsUI : MonoBehaviour
{
    private TextMeshProUGUI pointsText;

    public UnityEvent OnTextChange;

    private void Awake()
    {
        pointsText = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void Setpoints(int points)
    {
        pointsText.SetText(points.ToString());
        OnTextChange?.Invoke();
    }
}
