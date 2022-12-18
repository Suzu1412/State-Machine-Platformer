using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventListener : MonoBehaviour
{
    [SerializeField] private bool debugEvent;

    [SerializeField] private List<EventAndResponse> eventAndResponses = new List<EventAndResponse>();

    private void OnEnable()
    {
        if (eventAndResponses.Count >= 1)
        {
            foreach (EventAndResponse eventAndResponse in eventAndResponses)
            {
                eventAndResponse.GameEvent.Register(this);
            }
        }
    }

    private void OnDisable()
    {
        if (eventAndResponses.Count >= 1)
        {
            foreach (EventAndResponse eventAndResponse in eventAndResponses)
            {
                eventAndResponse.GameEvent.UnRegister(this);
            }
        }
    }



    [ContextMenu("Raise Events")]
    public void OnEventRaised(GameEvent passedEvent)
    {
        for (int i = eventAndResponses.Count - 1; i >= 0; i--)
        {
            // Check if the passed event is the correct one
            if (passedEvent == eventAndResponses[i].GameEvent)
            {
                if (debugEvent)
                {
                    Debug.Log("Called Event named: " + eventAndResponses[i].Name + " on GameObject: " + gameObject.name);
                }

                eventAndResponses[i].EventRaised();
            }
        }

    }
}
