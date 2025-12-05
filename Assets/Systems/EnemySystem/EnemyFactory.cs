using UnityEngine;

public class EnemyFactory
{
    private readonly EnemyPool pool;

    public EnemyFactory(EnemyConfigSO cfg, int poolSize, Transform parent)
    {
        pool = new EnemyPool(cfg.prefab, poolSize, parent);
    }

    public Enemy Spawn(EnemyConfigSO config, Vector2 pos, Transform target)
    {
        Enemy e = pool.Get();
        e.transform.position = pos;
        e.gameObject.SetActive(true);

        e.Init(config, target);
        return e;
    }
}
