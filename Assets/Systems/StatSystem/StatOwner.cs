using System;
using System.Collections.Generic;
using UnityEngine;

public class StatOwner : MonoBehaviour, IStatOwner
{
    [SerializeField] private StatDefinitionSO definition;

    private StatContainer mods;
    
    // Cache
    private readonly Dictionary<StatType, float> cache = new();
    private readonly HashSet<StatType> dirty = new();

    private void Awake()
    {
        foreach (StatType type in Enum.GetValues(typeof(StatType)))
        {
            dirty.Add(type);
        }
    }

    public float GetStat(StatType type)
    {
        if (dirty.Contains(type))
        {
            float final = StatCalculator.CalculateFinal(mods, definition, type);
            cache[type] = final;
            dirty.Remove(type);

            StatEvents.RaiseStatChanged(this, type, final);
        }

        return cache[type];
    }

    public void AddModifier(StatType type, StatModifier mod)
    {
        mods ??= new StatContainer();
        mods.Add(type, mod);

        MarkDirty(type);
    }

    public void RemoveModifier(StatType type, StatModifier mod)
    {
        mods?.Remove(type, mod);
        MarkDirty(type);
    }

    private void MarkDirty(StatType type)
    {
        dirty.Add(type);
    }
}
