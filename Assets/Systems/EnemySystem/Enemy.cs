using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(StatOwner))]
[RequireComponent(typeof(ResourceHealth))]
public class Enemy : MonoBehaviour, IDamageable, IDamaging
{
    private Rigidbody2D rb;
    private ResourceHealth health;

    public StatOwner Stats { get; private set; }
    public EnemyConfigSO Config { get; private set; }

    public GameObject source => gameObject;

    public System.Action<Enemy> OnDespawn;

    private IEnemyStrategy strategy;
    

    private float touchDamageCooldown = 0.5f;
    private float touchDamageTimer = 0f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Stats = GetComponent<StatOwner>();
        health = GetComponent<ResourceHealth>();

        health.OnHealthZero += Die;
    }

    public void Init(EnemyConfigSO cfg, Transform target)
{
    Config = cfg;
    
    strategy = new SwarmEnemyStrategy(this, target);

    health.Init(() => Stats.GetStat(StatType.MaxHP));
}


    private void Update()
    {
        float dt = Time.deltaTime;

        touchDamageTimer -= dt;
        strategy.Tick(dt);
    }

    public void Move(Vector2 velocity) => rb.linearVelocity = velocity;
    public void StopMoving() => rb.linearVelocity = Vector2.zero;

    
    public DamageInfo GetDamage()
    {
        return new DamageInfo(Config.damage, this);
    }

    
    public void ApplyDamage(DamageInfo dmg)
    {
        health.ApplyDamage(dmg.Amount);
    }

    private void Die()
    {
        StopMoving();
        OnDespawn?.Invoke(this);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        
    if (touchDamageTimer > 0f)
        return;
    
    if (!other.TryGetComponent<IDamageable>(out var dmgTarget))
        return;

    // only damage player OR non-enemy
    if (other.TryGetComponent<PlayerHealth>(out _) || !other.TryGetComponent<Enemy>(out _))
    {
        touchDamageTimer = touchDamageCooldown;
        dmgTarget.ApplyDamage(GetDamage());
    }


    }
}
