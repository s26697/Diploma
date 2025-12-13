using System;
using UnityEngine;

class EffectExecutor : MonoBehaviour
{
    [SerializeField]
    EffectDataSO effectData;
    

    public bool execute (GameObject target)
    {
        foreach (var effect in effectData.effects)
        {
            effect.Execute(gameObject, target);
        }
        return true;
    }
}