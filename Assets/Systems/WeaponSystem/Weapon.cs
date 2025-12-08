using System;
using UnityEngine;

public class Weapon 
{
    private readonly IDamaging _source;
    private readonly WeaponConfigSO _config;
    private readonly ProjectileFactory _projectileFactory;
    private readonly IStatOwner _stats;

    private float _cooldown;

    public Weapon(WeaponConfigSO config, ProjectileFactory projectileFactory, IStatOwner stats, IDamaging source)
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

    private bool CanAttack => _cooldown <= 0f;


    private float GetEffectiveAttackSpeed()
    {
        float baseAS = _config.baseAttackSpeed;

        if (_stats != null)
        {
            float bonus = _stats.GetStat(StatType.AttackSpeed);
            baseAS *= (1f + bonus);
        }

        return Mathf.Max(0.01f, baseAS);
    }

    private int GetProjectileCount()
    {
        if (_stats == null)
            return 1;

        
        int count = _config.baseProjectiles + Mathf.RoundToInt(_stats.GetStat(StatType.ProjectileCount));

        return Mathf.Max(1, count);
    }

    private Vector2 ApplyAccuracy(Vector2 direction)
    {
        // TODO — implement later
        return direction.normalized;
    }


    public void Attack(Vector2 origin, Vector2 direction)
    {
        if (!CanAttack)
            return;

        direction = ApplyAccuracy(direction);

        int projectileCount = GetProjectileCount();

        if (projectileCount == 1)
        {
            SpawnProjectile(origin, direction);
        }
        else
        {
            SpawnProjectileBurst(origin, direction, projectileCount);
        }

        _cooldown = 1f / GetEffectiveAttackSpeed();
    }


    private void SpawnProjectile(Vector2 origin, Vector2 direction)
    {
        _projectileFactory.Spawn(
            _config.projectileConfig,
            _stats,
            origin,
            direction,
            _source
        );
    }

    private void SpawnProjectileBurst(Vector2 origin, Vector2 direction, int count)
    {
        float angleStep = 10f; // TODO stały rozrzut
        float mid = (count - 1) * 0.5f;

        for (int i = 0; i < count; i++)
        {
            float offset = (i - mid) * angleStep;
            Vector2 finalDir = Quaternion.Euler(0, 0, offset) * direction;

            SpawnProjectile(origin, finalDir);
        }
    }
}
