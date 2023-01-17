using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DropDefinition
{
    [SerializeField] private PickableItem item;
    [Range(0f, 1f)]
    [SerializeField] private float dropChance; 

    public PickableItem Item => item;
    public float DropChance => dropChance;

}
