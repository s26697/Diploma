using UnityEngine;

public class ProjectileFactory 
{
    
    private int poolSize = 50;
    private ProjectilePool pool;

     public ProjectileFactory( int PoolSize = 50)
    {
        this.poolSize = PoolSize;

    }
    

    public void Spawn(ProjectileConfigSO config, IStatOwner owner, Vector2 position, Vector2 direction, GameObject source)
    {
        if(pool == null)
        {
            pool = new ProjectilePool(config.prefab,poolSize);
        }
        // #todo dodać statystyki z których korzysta z statsystemu
        ProjectileRuntimeStats stats = new ProjectileRuntimeStats
        {
            
            speed = config.speed, //* owner.GetStat(StatType.ProjectileSpeed),
            maxDistance = config.maxDistance, //* owner.GetStat(StatType.Range),
            damage = config.damage, //* owner.GetStat(StatType.DamageMultiplier),
            lifetime = config.lifetime //* owner.GetStat(StatType.ProjectileLifetime)
            
        };

        
        Projectile p = pool.Get();
        p.transform.position = position;
        p.Init(config, direction, stats, source);
    }
}
