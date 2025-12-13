using System;
using JetBrains.Annotations;
using UnityEngine;

[Serializable]
public abstract class Effect
{
    public abstract bool Execute(GameObject source, GameObject target);
}




