using UnityEngine;
[RequireComponent(typeof(ResourceHealth))]
public class PlayerHealth : MonoBehaviour, IDamageable
{
    private ResourceHealth health;
    private StatOwner stats;

    private void Awake()
    {
        stats = GetComponent<StatOwner>();
        health = GetComponent<ResourceHealth>();

        

        health.OnHealthZero += Die;
    }

    private void Start()
    {
        health.Init(() => stats.GetStat(StatType.MaxHP));
    }

    public void ApplyDamage(DamageInfo dmg)
    {
         if (health == null) return;
        health.ApplyDamage(dmg.Amount);
        Debug.Log($"PLAYER HIT for {dmg.Amount}");
    }

    private void Die()
    {
        Debug.Log("PLAYER DEAD");
    }
}
