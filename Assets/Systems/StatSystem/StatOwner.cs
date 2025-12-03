using System;
using System.Collections.Generic;
using UnityEngine;

public class StatOwner : MonoBehaviour
{
    [SerializeField] private StatDefinitionSO definition;

    private StatContainer runtimeMods;

    // Cache
    private readonly Dictionary<StatType, float> currentValues = new();
    private readonly HashSet<StatType> dirty = new();

    private void Awake()
    {
        
        foreach (StatType type in Enum.GetValues(typeof(StatType)))
        {
            currentValues[type] = definition.GetBaseValue(type);
            dirty.Add(type); 
        }
    }

    public float GetStat(StatType type)
    {
        if (dirty.Contains(type))
        {
            Recalculate(type);
            dirty.Remove(type);
        }

        return currentValues[type];
    }

    private void Recalculate(StatType type)
    {
        float baseValue = definition.GetBaseValue(type);

        if (runtimeMods == null)
        {
            currentValues[type] = baseValue;
            return;
        }

        float final = runtimeMods.GetFinalValue(type, baseValue);
        currentValues[type] = final;
    }

    public void AddModifier(StatType type, StatModifier modifier)
    {
        runtimeMods ??= new StatContainer();
        runtimeMods.AddModifier(type, modifier);

        dirty.Add(type); 
    }

    
}
