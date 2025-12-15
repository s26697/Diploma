using System.Collections.Generic;
using UnityEngine;

public  class ProjectilePool
{
    private readonly Queue<Projectile> pool = new();
    private readonly Projectile prefab;

    public ProjectilePool(Projectile prefab, int initialSize)
    {
        this.prefab = prefab;

        for (int i = 0; i < initialSize; i++)
            CreateNew();
    }

    private Projectile CreateNew()
    {
        var p = GameObject.Instantiate(prefab);
        p.gameObject.SetActive(false);
        p.OnDespawn = ReturnToPool;
        pool.Enqueue(p);
        return p;
    }

    private void ReturnToPool(Projectile p)
    {
        p.gameObject.SetActive(false);
        pool.Enqueue(p);
    }

    public Projectile Get()
    {
        if (pool.Count == 0)
            CreateNew();

        var p = pool.Dequeue();
        p.gameObject.SetActive(true);
        return p;
    }
}