using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Stat
{
    protected StatDefinitionSO statDefinition;
    protected int value;
    public int Value => value;

    public virtual int baseValue => statDefinition.BaseValue;

    public event Action valueChanged;

    protected List<StatModifier> modifiers = new List<StatModifier>();

    public Stat(StatDefinitionSO statDefinition)
    {
        this.statDefinition = statDefinition;
        CalculateValue();
    }

    public void AddModifier(StatModifier modifier)
    {
        modifiers.Add(modifier);
        CalculateValue();
    }

    public void RemoveModifierFromSource(UnityEngine.Object source)
    {
        modifiers = modifiers.Where(m => m.source.GetInstanceID() != source.GetInstanceID()).ToList();
        CalculateValue();
    }

    protected void CalculateValue()
    {
        int finalValue = baseValue;

        modifiers.Sort((x, y) => x.type.CompareTo(y.type));

        for (int i = 0; i < modifiers.Count; i++)
        {
            StatModifier modifier = modifiers[i];

            if (modifier.type == ModifierOperationType.Additive)
            {
                finalValue += modifier.magnitude;
            }
            else if (modifier.type == ModifierOperationType.Multiplicative)
            {
                finalValue *= modifier.magnitude;
            }
        }

        if (statDefinition.MaxValue >= 0)
        {
            finalValue = Mathf.Min(finalValue, statDefinition.MaxValue);
        }

        // Only if the value changed the value will be assigned and trigger the event 
        if (value != finalValue)
        {
            value = finalValue;
            valueChanged?.Invoke();
        }
    }
}
