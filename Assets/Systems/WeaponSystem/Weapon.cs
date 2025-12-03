using UnityEngine;

public class Weapon
{
    private readonly WeaponConfigSO _config;
    private readonly ProjectileFactory _projectileFactory;
    private readonly IStatOwner _stats;

    private float _cooldown;

    public Weapon(WeaponConfigSO config, ProjectileFactory projectileFactory, IStatOwner stats)
    {
        _config = config;
        _projectileFactory = projectileFactory;
        _stats = stats;
    }

   
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

    /* #TODO na razie idc about accuracy
    private float GetAccuracy()
    {
        float acc = _config.baseAccuracy;

        if (_stats != null)
        {
            float bonus = _stats.GetStat(StatType.WeaponAccuracy);
            acc *= (1f + bonus);
        }

        return Mathf.Clamp01(acc);
    }
*/
    public void Tick(float dt)
    {
        if (_cooldown > 0)
            _cooldown -= dt;
    }

    private Vector2 handleDirection()
    {
        /* TODO
        float accuracy = GetAccuracy();
        float maxAngleOffset = (1f - accuracy) * 10f;
        float angle = Random.Range(-maxAngleOffset, maxAngleOffset);
        Quaternion.Euler(0, 0, angle) * direction;
        */

        return new Vector2(0,0);
    }

    public void Attack(Vector2 origin, Vector2 direction)
    {
        if (_cooldown > 0)
            return;

        
        
       // direction = handleAccuracy();

        _projectileFactory.Spawn(_config.projectileConfig, _stats, origin, direction);

        _cooldown = 1f / GetEffectiveAttackSpeed();
    }
}
