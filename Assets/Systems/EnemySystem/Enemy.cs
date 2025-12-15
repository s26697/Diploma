using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(StatOwner))]
[RequireComponent(typeof(ResourceHealth))]
[RequireComponent(typeof(WeaponSystem))]
public class Enemy : MonoBehaviour, IDamageable, IDamaging
{
    private Rigidbody2D rb;
    private ResourceHealth health;
    private WeaponSystem weapon;

    public StatOwner Stats { get; private set; }
    public EnemyConfigSO Config { get; private set; }

    public GameObject source => gameObject;

    private EnemyContext context;

    public System.Action<Enemy> OnDespawn;

    private float touchDamageCooldown = 0.5f;
    private float touchDamageTimer = 0f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Stats = GetComponent<StatOwner>();
        health = GetComponent<ResourceHealth>();
        weapon = GetComponent<WeaponSystem>();

        health.OnHealthZero += Die;
    }

    public void Init(EnemyConfigSO cfg, Transform target)
    {
        Config = cfg;

        health.Init(() => Stats.GetStat(StatType.MaxHP));

        context = new EnemyContext(
            this,
            target,
            Stats,
            Config,
            weapon
        );
    }

    private void FixedUpdate()
    {
        float dt = Time.deltaTime;
        touchDamageTimer -= dt;

        Config.strategy?.Tick(context, dt);
    }

    public void Move(Vector2 velocity) => rb.linearVelocity = velocity;
    public void StopMoving() => rb.linearVelocity = Vector2.zero;

    public DamageInfo GetDamage() =>
        new DamageInfo(Config.damage, this);

    public void ApplyDamage(DamageInfo dmg)
    {
        health.ApplyDamage(dmg.Amount);
    }

    private void Die()
    {
        StopMoving();
        GameEvents.EnemyDied(this);
        OnDespawn?.Invoke(this);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (touchDamageTimer > 0f)
            return;

        if (!other.TryGetComponent<IDamageable>(out var dmgTarget))
            return;

        if (other.TryGetComponent<Enemy>(out _))
            return;

        if (other.TryGetComponent<PlayerHealth>(out _))
        {
            touchDamageTimer = touchDamageCooldown;
            dmgTarget.ApplyDamage(GetDamage());
        }
    }
}
