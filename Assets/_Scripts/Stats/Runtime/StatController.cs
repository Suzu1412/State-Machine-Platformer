using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatController : MonoBehaviour
{
    [SerializeField] private StatDatabaseSO statDatabase;

    protected Dictionary<string, Stat> stats = new Dictionary<string, Stat>(StringComparer.OrdinalIgnoreCase);

    public Dictionary<string, Stat> Stats => stats;

    private bool isInitialized;

    public bool IsInitialized => isInitialized;

    public event Action initialized;

    public event Action willUnintialize;

    private void Awake()
    {
        if (!isInitialized)
        {
            Initialize();
            isInitialized = true;
            initialized?.Invoke();
        }
    }

    private void OnDestroy()
    {
        willUnintialize?.Invoke();
    }

    private void Initialize()
    {
        foreach (StatDefinitionSO definition in statDatabase.stats)
        {
            stats.Add(definition.name, new Stat(definition));
        }
    }
}
