using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DecisionValueComparerSO : DecisionSO
{
    [SerializeField] protected ComparerType comparer;
    [SerializeField] protected float value;

    protected bool ValueCompare(float amount)
    {
        return comparer switch
        {
            ComparerType.MoreThanOrEqual => amount >= value,
            ComparerType.MoreThan => amount > value,
            ComparerType.EqualTo => amount == value,
            ComparerType.LessThan => amount < value,
            ComparerType.LessThanOrEqual => amount <= value,
            _ => false,
        };
    }
}
