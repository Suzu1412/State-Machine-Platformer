using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attribute : Stat
{
    protected int currentValue;

    public int CurrentValue => currentValue;

    public event Action currentValueChanged;
    public event Action<StatModifier> appliedModifier;

    public Attribute(StatDefinitionSO definition) : base(definition)
    {
        currentValue = value;
    }

    public virtual void ApplyModifier(StatModifier modifier)
    {
        int newValue = currentValue;

        switch (modifier.type)
        {
            case ModifierOperationType.Additive:
                newValue += modifier.magnitude;
                break;
            case ModifierOperationType.Multiplicative:
                newValue *= modifier.magnitude;
                break;
            case ModifierOperationType.Override:
                newValue = modifier.magnitude;
                break;
            default:
                break;
        }

        if (statDefinition.MaxValue >= 0)
        {
            newValue = Mathf.Clamp(newValue, 0, value);
        }

        if (currentValue != newValue)
        {
            currentValue = newValue;
            currentValueChanged?.Invoke();
            appliedModifier?.Invoke(modifier);
        }
    }
}
