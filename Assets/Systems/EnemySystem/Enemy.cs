using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(StatOwner))]
public class Enemy : MonoBehaviour, IDamageable
{
    private Rigidbody2D rb;

    public StatOwner Stats { get; private set; }
    public EnemyConfigSO Config { get; private set; }

    private EnemyStrategy strategy;

    public System.Action<Enemy> OnDespawn;

    private Transform target;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Stats = GetComponent<StatOwner>();
    }

    public void Init(EnemyConfigSO cfg, Transform target)
    {
        Config = cfg;
        this.target = target;
        strategy = new EnemyStrategy(this, target);
    }

    private void Update()
    {
        float dt = Time.deltaTime;
        strategy.Tick(dt);

    }

    public void Move(Vector2 velocity)
    {
        rb.linearVelocity = velocity;
    }

    public void StopMoving()
    {
        rb.linearVelocity = Vector2.zero;
    }

    public void DealDamage(float dmg)
    {
        // TODO: damage to player
        Debug.Log($"Enemy dealt {dmg} dmg");
    }

    private void Despawn()
    {
        rb.linearVelocity = Vector2.zero;
        OnDespawn?.Invoke(this);
    }

    public void TakeDamage(DamageInfo dmg)
    {
        throw new System.NotImplementedException();
    }
}
