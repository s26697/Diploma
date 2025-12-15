using System.Collections.Generic;
using UnityEngine;

public class EnemyPool
{
    private readonly Queue<Enemy> pool = new();
    private readonly Enemy prefab;
    private readonly Transform parent;

    public EnemyPool(Enemy prefab, int size, Transform parent)
    {
        this.prefab = prefab;
        this.parent = parent;

        for (int i = 0; i < size; i++)
        {
            var e = GameObject.Instantiate(prefab, parent);
            e.gameObject.SetActive(false);
            e.OnDespawn = ReturnToPool;
            pool.Enqueue(e);
        }
    }

    public Enemy Get()
    {
        if (pool.Count == 0)
        {
            var e = GameObject.Instantiate(prefab, parent);
            e.OnDespawn = ReturnToPool;
            return e;
        }

        var enemy = pool.Dequeue();
        enemy.gameObject.SetActive(true);
        return enemy;
    }

    private void ReturnToPool(Enemy e)
    {
        e.gameObject.SetActive(false);
        pool.Enqueue(e);
    }
}
