using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    private ResourceHealth health;
    private StatOwner stats;

    private void Awake()
    {
        stats = GetComponent<StatOwner>();
        health = GetComponent<ResourceHealth>();

        health.Init(() => stats.GetStat(StatType.MaxHP));

        health.OnHealthZero += Die;
    }

    public void ApplyDamage(DamageInfo dmg)
    {
        health.ApplyDamage(dmg.Amount);
        Debug.Log($"PLAYER HIT for {dmg.Amount}");
    }

    private void Die()
    {
        Debug.Log("PLAYER DEAD");
    }
}
