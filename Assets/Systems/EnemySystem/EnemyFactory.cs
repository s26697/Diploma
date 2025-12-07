using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory
{
    private readonly Transform parent;

    private readonly Dictionary<EnemyConfigSO, EnemyPool> pools = new();

    private readonly int defaultPoolSize;

    public EnemyFactory(int defaultPoolSize, Transform parent)
    {
        this.defaultPoolSize = defaultPoolSize;
        this.parent = parent;
    }

    public Enemy Spawn(EnemyConfigSO config, Vector2 pos, Transform target)
    {
        if (!pools.TryGetValue(config, out var pool))
        {
            
            pool = new EnemyPool(config.prefab, defaultPoolSize, parent);
            pools[config] = pool;
        }

        
        Enemy enemy = pool.Get();
        enemy.transform.position = pos;
        enemy.gameObject.SetActive(true);

        enemy.Init(config, target);

        return enemy;
    }
    
}
