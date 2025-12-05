using System.Collections.Generic;
using UnityEngine;

public class EnemyPool
{
    private readonly Stack<Enemy> pool = new();
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
            pool.Push(e);
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

        return pool.Pop();
    }

    private void ReturnToPool(Enemy e)
    {
        e.gameObject.SetActive(false);
        pool.Push(e);
    }
}
