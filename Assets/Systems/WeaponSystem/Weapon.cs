using System;
using UnityEngine;

public class Weapon 
{
    private readonly GameObject _source;
    private readonly WeaponConfigSO _config;
    private readonly ProjectileFactory _projectileFactory;
    private readonly IStatOwner _stats;

    private float _cooldown;

    public Weapon(WeaponConfigSO config, ProjectileFactory projectileFactory, IStatOwner stats, GameObject source)
    {
        _config = config;
        _projectileFactory = projectileFactory;
        _stats = stats;
        _source = source;
    }

    public void Tick(float dt)
    {
        if (_cooldown > 0f)
            _cooldown -= dt;
    }

   public void Attack(Vector2 origin, Vector2 direction)
{
    if (_cooldown > 0f)
        return;

    if (_config.attackStrategy == null)
    {
        Debug.LogWarning("Weapon has no attack strategy!");
        return;
    }

    int projectileCount = GetProjectileCount();
    float accuracy = _config.baseAccuracy;

    var ctx = new AttackContext(
        origin,
        direction,
        _projectileFactory,
        _stats,               
        _source,
        _config.projectileConfig,
        projectileCount,
        accuracy
    );

    _config.attackStrategy.Attack(ctx);
    _cooldown = 1f / GetEffectiveAttackSpeed();
}

    private float GetEffectiveAttackSpeed()
    {
        float aspeed = _config.baseAttackSpeed;

        if (_stats != null)
            aspeed *= 1f + _stats.GetStat(StatType.AttackSpeed);

        return Mathf.Max(0.01f, aspeed);
    }

    private int GetProjectileCount()
{
    int count = _config.baseProjectiles;

    if (_stats != null)
        count += Mathf.RoundToInt(_stats.GetStat(StatType.ProjectileCount));

    return Mathf.Max(1, count);
}

private float GetAccuracy()
{
    float acc = _config.baseAccuracy;

    return Mathf.Clamp01(acc);
}

}
