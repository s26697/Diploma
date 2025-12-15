using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(EffectExecutor))]
public class Projectile : MonoBehaviour
{
    private ProjectileConfigSO config;
    private ProjectileRuntimeStats stats;
    
    private EffectExecutor executor;

    private GameObject sourceGO;
    private Rigidbody2D rb;

    private Vector2 direction;
    private Vector2 startPos;

    private float timeAlive;
    private bool hasHit = false;


    public System.Action<Projectile> OnDespawn;

    private Collider2D col;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        executor = GetComponent<EffectExecutor>();
    }

    public void Init(ProjectileConfigSO config, Vector2 direction, ProjectileRuntimeStats runtimeStats, GameObject sourceGO)
    {
        this.config = config;
        this.stats = runtimeStats;
        this.sourceGO = sourceGO;
        executor.SetSource(sourceGO);

        this.direction = direction.normalized;
        this.startPos = transform.position;
        this.timeAlive = 0f;
        this.hasHit = false;

        rb.linearVelocity = this.direction * stats.speed;
    }

    private void OnEnable()
{
    hasHit = false;
    timeAlive = 0f;

    rb.simulated = true;
    col.enabled = true;
}


    
    private void FixedUpdate()
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
        if (!rb.simulated) return;
        if (hasHit) return;
        if (other.gameObject == sourceGO) return;

        if (executor.Execute(other.gameObject))
        {
            hasHit = true;
            Despawn();
        }
    }

    private void Despawn()
{
    hasHit = true;

    rb.linearVelocity = Vector2.zero;
    rb.simulated = false;     
    col.enabled = false;      

    OnDespawn?.Invoke(this);
}

}
