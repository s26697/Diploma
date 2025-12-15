using System;
using UnityEngine;

[Serializable]
public sealed class SwarmEnemyStrategy : IEnemyStrategy
{
    public void Tick(in EnemyContext ctx, float dt)
    {
        if (ctx.Target == null)
        {
            ctx.Enemy.StopMoving();
            return;
        }

        Vector2 dir =
            (ctx.Target.position - ctx.Enemy.transform.position).normalized;

        float speed = ctx.Stats.GetStat(StatType.MoveSpeed);
        ctx.Enemy.Move(dir * speed);
    }
}
