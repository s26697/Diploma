using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EffectData", menuName = "Game/Effect")]
 public class EffectDataSO : ScriptableObject
{
    public string label;
    [SerializeReference]
    public List<Effect> effects;

    void OnEnable()
    {
        if (string.IsNullOrEmpty(label)) label = name;
        if (effects == null) effects = new List<Effect>();
    }


}

