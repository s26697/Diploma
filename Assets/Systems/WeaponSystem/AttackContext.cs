using UnityEngine;

public readonly struct AttackContext
{
    public readonly Vector2 Origin;
    public readonly Vector2 Direction;

    public readonly ProjectileFactory Factory;
    public readonly IStatOwner StatOwner;
    public readonly GameObject Source;

    public readonly ProjectileConfigSO Projectile;
    public readonly int ProjectileCount;
    public readonly float Accuracy;

    public AttackContext(
        Vector2 origin,
        Vector2 direction,
        ProjectileFactory factory,
        IStatOwner statOwner,
        GameObject source,
        ProjectileConfigSO projectile,
        int projectileCount,
        float accuracy)
    {
        Origin = origin;
        Direction = direction.normalized;

        Factory = factory;
        StatOwner = statOwner;
        Source = source;

        Projectile = projectile;
        ProjectileCount = Mathf.Max(1, projectileCount);
        Accuracy = Mathf.Clamp01(accuracy);
    }
}
