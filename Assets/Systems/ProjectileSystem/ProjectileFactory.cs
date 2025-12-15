using System.Collections.Generic;
using UnityEngine;

public class ProjectileFactory
{
    private readonly int defaultPoolSize;
    private readonly Dictionary<ProjectileConfigSO, ProjectilePool> pools = new();

    public ProjectileFactory(int defaultPoolSize = 50)
    {
        this.defaultPoolSize = defaultPoolSize;
    }

    public void Spawn(ProjectileConfigSO config, IStatOwner owner, Vector2 position, Vector2 direction, GameObject source)
    {
        if (config == null || config.prefab == null)
        {
            Debug.LogWarning("[ProjectileFactory] Config or prefab is null!");
            return;
        }

        
        if (!pools.TryGetValue(config, out var pool))
        {
            pool = new ProjectilePool(config.prefab, defaultPoolSize);
            pools[config] = pool;
        }

        
        ProjectileRuntimeStats stats = new ProjectileRuntimeStats
        {
            speed = config.speed,
            maxDistance = config.maxDistance,
            damage = config.damage,
            lifetime = config.lifetime
        };

        
        Projectile projectile = pool.Get();
        projectile.transform.position = position;

        
        projectile.Init(config, direction, stats, source);
    }
}