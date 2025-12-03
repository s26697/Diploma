using System.Collections.Generic;
using UnityEngine;

public class ProjectilePool
{
    private readonly Queue<Projectile> pool = new();
    private readonly Projectile prefab;
    private readonly Transform parent;

    public ProjectilePool(Projectile prefab, int initialSize, Transform parent = null)
    {
        this.prefab = prefab;
        this.parent = parent;

        for (int i = 0; i < initialSize; i++)
            CreateNew();
    }

    private Projectile CreateNew()
    {
        var p = GameObject.Instantiate(prefab, parent);
        p.gameObject.SetActive(false);
        p.OnDespawn = ReturnToPool;
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
            pool.Enqueue(CreateNew());

        var p = pool.Dequeue();
        p.gameObject.SetActive(true);
        return p;
    }
}
