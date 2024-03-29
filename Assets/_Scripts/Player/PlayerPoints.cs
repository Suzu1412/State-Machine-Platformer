using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerPoints : MonoBehaviour
{
    public UnityEvent<int> OnPointsValueChange;
    public UnityEvent OnPickUpPoints;
    private int points = 0;

    public int Points { get => points; private set => points = value; }

    private void Start()
    {
        OnPointsValueChange?.Invoke(Points);
    }

    public void Add(int amount)
    {
        Points += amount;
        OnPickUpPoints?.Invoke();
        OnPointsValueChange?.Invoke(Points);
    }
}