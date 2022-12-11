using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StatSystem/StatDefinition", fileName = "Stat Definition")]
public class StatDefinitionSO : ScriptableObject
{
    [SerializeField] private int baseValue;
    [SerializeField] private int maxValue;

    public int BaseValue => baseValue;

    public int MaxValue => maxValue;
}
