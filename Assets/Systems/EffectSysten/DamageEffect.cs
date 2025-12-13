using System;
using UnityEngine;

[Serializable]
public class DamageEffect : Effect
{
    public float amount;

    public override bool Execute(GameObject source, GameObject target)
    {
        if (target == source) return false;

        if (!target.TryGetComponent<IDamageable>(out var hp)) return false;
        if (!source.TryGetComponent<IDamaging>(out var dmgable)) return false;

        hp.ApplyDamage(new DamageInfo(amount, dmgable));
        return true;
    }
}
