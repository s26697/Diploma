using System;
using UnityEngine;

[Serializable]
public sealed class StraightAttackStrategy : IAttackStrategy
{
    [Header("Spread")]
    [Range(0f, 30f)]
    public float maxSpreadAngle = 10f;

    [Header("Multi projectile")]
    [Range(0f, 0.5f)]
    public float spawnSpacing = 0.15f;

    public void Attack(AttackContext ctx)
    {
        float spread = Mathf.Lerp(maxSpreadAngle, 0f, ctx.Accuracy);

        Vector2 forward = ctx.Direction.normalized;
        Vector2 right = new Vector2(-forward.y, forward.x);

        int count = ctx.ProjectileCount;
        float center = (count - 1) * 0.5f;

        for (int i = 0; i < count; i++)
        {
           
            Vector2 spawnPos =
                ctx.Origin + right * (i - center) * spawnSpacing;

            Vector2 dir = ApplySpread(forward, spread);

            ctx.Factory.Spawn(
                ctx.Projectile,
                ctx.StatOwner,
                spawnPos,
                dir,
                ctx.Source
            );
        }
    }

    private Vector2 ApplySpread(Vector2 dir, float spread)
    {
        if (spread <= 0f)
            return dir;

        float angle = UnityEngine.Random.Range(-spread, spread);
        return Quaternion.Euler(0f, 0f, angle) * dir;
    }
}
