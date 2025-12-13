using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(EffectExecutor))]
public class Projectile : MonoBehaviour
{
    private ProjectileConfigSO config;
    private ProjectileRuntimeStats stats;
    
    private EffectExecutor executor;

    private IDamaging source;
    private Rigidbody2D rb;

    private Vector2 direction;
    private Vector2 startPos;

    private float timeAlive;
    private bool hasHit = false;


    public System.Action<Projectile> OnDespawn;

    

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        executor = GetComponent<EffectExecutor>();
    }

    public void Init(ProjectileConfigSO config, Vector2 direction, ProjectileRuntimeStats runtimeStats, IDamaging source)
    {
        this.config = config;
        this.stats = runtimeStats;
        this.source = source;

        this.direction = direction.normalized;
        this.startPos = transform.position;
        this.timeAlive = 0f;
        this.hasHit = false;

        rb.linearVelocity = this.direction * stats.speed;
    }

    

    
    private void Update()
    {
        
        CheckDespawnConditions();
        RotateToMovement();
    }

    
    private void CheckDespawnConditions()
    {
        timeAlive += Time.deltaTime;

        if (timeAlive >= stats.lifetime)
        {
            Despawn();
            return;
        }

        if (Vector2.Distance(startPos, transform.position) >= stats.maxDistance)
        {
            Despawn();
            return;
        }
    }

    private void RotateToMovement()
    {
        Vector2 velocity = rb.linearVelocity;
        if (velocity.sqrMagnitude > 0.01f)
            transform.right = velocity;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasHit) return;

        if (executor.execute(other.gameObject))
        {
            hasHit = true;
            Despawn();
        }
    }

    private void Despawn()
    {
        rb.linearVelocity = Vector2.zero;
        OnDespawn?.Invoke(this);
    }
}
