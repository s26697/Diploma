using UnityEngine;

public readonly struct EnemyContext
{
    public readonly Enemy Enemy;
    public readonly Transform Target;
    public readonly StatOwner Stats;
    public readonly EnemyConfigSO Config;
    public readonly WeaponSystem Weapon;

    public EnemyContext(
        Enemy enemy,
        Transform target,
        StatOwner stats,
        EnemyConfigSO config,
        WeaponSystem weapon)
    {
        Enemy = enemy;
        Target = target;
        Stats = stats;
        Config = config;
        Weapon = weapon;
    }
}
