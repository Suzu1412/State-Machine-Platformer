using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

[CreateAssetMenu(menuName = "Game Event", fileName = "GameEvent")]
public class GameEvent : ScriptableObject
{
    [SerializeField] private string sentString;
    [SerializeField] private int sentInt;
    [SerializeField] private float sentFloat;
    [SerializeField] private bool sentBool;
    [SerializeField] private GameObject sentGameObject;

    public string SentString => sentString;
    public int SentInt => sentInt;
    public float SentFloat => sentFloat;
    public bool SentBool => sentBool;
    public GameObject SentGameObject => sentGameObject;

    private List<EventListener> eventListeners = new List<EventListener>();

    public void Raise()
    {
        for (int i = eventListeners.Count - 1; i >= 0; i--)
        {
            eventListeners[i].OnEventRaised(this);
        }
    }

    public void Register(EventListener passedEvent)
    {

        if (!eventListeners.Contains(passedEvent))
        {
            eventListeners.Add(passedEvent);
        }

    }

    public void UnRegister(EventListener passedEvent)
    {

        if (eventListeners.Contains(passedEvent))
        {
            eventListeners.Remove(passedEvent);
        }

    }
}
