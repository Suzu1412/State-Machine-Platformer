using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnTrigger2DUtil : MonoBehaviour
{
    [SerializeField] private LayerMask collissionMask;

    public UnityEvent OnTriggerEnter, OnTriggerExit;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        

    }
}
