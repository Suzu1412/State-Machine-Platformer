using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEvent : MonoBehaviour
{
    public void EventGeneric()
    {
        Debug.Log("This event is generic");
    }

    public void EventInt(int value)
    {
        Debug.Log("Int value: " + value);
    }
    public void EventString(string value)
    {
        Debug.Log("String value: " + value);
    }
    public void EventFloat(float value)
    {
        Debug.Log("Float value: " + value);
    }
    public void EventBool(bool value)
    {
        Debug.Log("Bool value: " + value);
    }
}
