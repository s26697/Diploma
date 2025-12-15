using System;
using UnityEngine;

[Serializable]
public sealed class RangedEnemyStrategy : IEnemyStrategy
{
    

    public void Tick(in EnemyContext ctx, float dt)
    {
        if (ctx.Target == null)
        {
            ctx.Enemy.StopMoving();
            return;
        }

        Vector2 enemyPos = ctx.Enemy.transform.position;
        Vector2 targetPos = ctx.Target.position;

        Vector2 dir = (targetPos - enemyPos);
        float distance = dir.magnitude;

        dir.Normalize();

        // === MOVE UNTIL IN ATTACK RANGE ===
        if (distance > ctx.Config.attackRange)
        {
            float speed = ctx.Stats.GetStat(StatType.MoveSpeed);
            ctx.Enemy.Move(dir * speed);
            return;
        }

        // === IN RANGE ===
        ctx.Enemy.StopMoving();
        ctx.Weapon.TryAttack(dir);
    }
}
