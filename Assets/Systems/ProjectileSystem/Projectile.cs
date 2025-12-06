using UnityEngine;

public class Projectile : MonoBehaviour, IDamaging
{
    private ProjectileConfigSO config;
    private ProjectileRuntimeStats currentStats;

    private Vector2 direction;
    private Vector2 startPos;
    private float timeAlive;

    private Rigidbody2D rb;

    private IDamaging source; 

    public System.Action<Projectile> OnDespawn;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Init(ProjectileConfigSO cfg, Vector2 dir, ProjectileRuntimeStats stats, IDamaging source)
    {
        config = cfg;
        direction = dir.normalized;
        timeAlive = 0f;
        startPos = transform.position;
        this.source = source;

        currentStats = stats;

        rb.linearVelocity = direction * currentStats.speed;
    }

    public DamageInfo GetDamage()
    {
        return new DamageInfo(currentStats.damage, source);
    }

    private void Update()
    {
        timeAlive += Time.deltaTime;

        if (config.rotateToVelocity)
            transform.right = rb.linearVelocity;

        if (timeAlive >= currentStats.lifetime)
            Despawn();

        if (Vector2.Distance(startPos, transform.position) >= currentStats.maxDistance)
            Despawn();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<IDamageable>(out var dmg))
        {
            dmg.ApplyDamage(GetDamage());
            Despawn();
        }
    }

    private void Despawn()
    {
        rb.linearVelocity = Vector2.zero;
        OnDespawn?.Invoke(this);
    }
}
