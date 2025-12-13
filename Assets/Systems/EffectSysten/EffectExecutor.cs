using System;
using UnityEngine;

public class EffectExecutor : MonoBehaviour
{
    [SerializeField] private EffectDataSO effectData;

    private GameObject source;

    public void SetSource(GameObject source)
    {
        this.source = source;
    }

    public bool Execute(GameObject target)
    {
        if (effectData == null || effectData.effects.Count == 0)
            return false;

        bool anyExecuted = false;

        foreach (var effect in effectData.effects)
        {
            if (effect.Execute(source, target))
                anyExecuted = true;
        }

        return anyExecuted;
    }
}
