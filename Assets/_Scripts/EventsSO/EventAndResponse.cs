using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class EventAndResponse 
{
    [SerializeField] private string name;
    [SerializeField] private GameEvent gameEvent;
    [SerializeField] private UnityEvent response;
    [SerializeField] private StringEvent responseForSentString;
    [SerializeField] private IntEvent responseForSentInt;
    [SerializeField] private FloatEvent responseForSentFloat;
    [SerializeField] private BoolEvent responseForSentBool;
    [SerializeField] private BoolEvent responseForSentGameObject;

    public string Name => name;
    public GameEvent GameEvent => gameEvent;

    public void EventRaised()
    {
        // default/generic
        if (response.GetPersistentEventCount() >= 1) // always check if at least 1 object is listening for the event
        {
            response.Invoke();
        }

        // string
        if (responseForSentString.GetPersistentEventCount() >= 1)
        {
            responseForSentString.Invoke(gameEvent.SentString);
        }

        // int
        if (responseForSentInt.GetPersistentEventCount() >= 1)
        {
            responseForSentInt.Invoke(gameEvent.SentInt);
        }

        // float
        if (responseForSentFloat.GetPersistentEventCount() >= 1)
        {
            responseForSentFloat.Invoke(gameEvent.SentFloat);
        }

        // bool
        if (responseForSentBool.GetPersistentEventCount() >= 1)
        {
            responseForSentBool.Invoke(gameEvent.SentBool);
        }

        // Game Object
        if (response.GetPersistentEventCount() >= 1)
        {
            responseForSentGameObject.Invoke(gameEvent.SentGameObject);
        }
    }
}
