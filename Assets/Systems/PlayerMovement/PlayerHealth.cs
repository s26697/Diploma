using UnityEngine;

[RequireComponent(typeof(ResourceHealth))]
[RequireComponent(typeof(StatOwner))]
public class PlayerHealth : MonoBehaviour, IDamageable, IDamaging
{
    private ResourceHealth health;
    private StatOwner stats;

    public GameObject source => gameObject;

    private void Awake()
    {
        stats = GetComponent<StatOwner>();
        health = GetComponent<ResourceHealth>();

        BindEvents();
    }

    private void Start()
    {
        InitializeHealth();
    }

    private void BindEvents()
    {
        health.OnHealthZero += Die;
    }

    private void InitializeHealth()
    {
        health.Init(() => stats.GetStat(StatType.MaxHP));
    }

    public void ApplyDamage(DamageInfo dmg)
    {
        if (dmg.Amount <= 0) return;

        health.ApplyDamage(dmg.Amount);
        Debug.Log($"PLAYER HIT for {dmg.Amount}");
    }

    private void Die()
    {
        Debug.Log("PLAYER DEAD");
        // #TODO dodac i obsluzyc even RaisePlayerDied();
    }

    public DamageInfo GetDamage()
    {
        throw new System.NotImplementedException();
    }
}
