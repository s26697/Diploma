using UnityEngine;

public interface IDamaging
{
    
    GameObject source { get; }
    DamageInfo GetDamage();
}