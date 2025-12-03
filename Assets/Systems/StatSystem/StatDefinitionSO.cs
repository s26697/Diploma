using System;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewStatDefinition", menuName = "Stats/StatDefinition")]
public class StatDefinitionSO : ScriptableObject
{
    [Tooltip("Bazowe wartości wszystkich statów.")]
    public StatEntry[] entries;

    [Serializable]
    public struct StatEntry
    {
        public StatType type;
        [Tooltip("Bazowa wartość statystyki.")]
        public float baseValue;
    }

    private Dictionary<StatType, float> _dict;

    public float GetBaseValue(StatType type)
    {
        if (_dict == null)
        {
            _dict = new Dictionary<StatType, float>();
            foreach (var e in entries)
                _dict[e.type] = e.baseValue;
        }

        return _dict[type];
    }
}
