
using System;
using UnityEngine;

[Serializable]
public class StatModifier
{
    [SerializeField]
    public float Value;
     [SerializeField]
    public StatModType Type;

    public StatModifier(float value, StatModType type)
    {
        Value = value;
        Type = type;
    }
}
