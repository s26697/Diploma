using UnityEngine;

public class EnemyStrategy //#todo inferfejs IEnemyStrategy
{
    private readonly Enemy enemy;
    private readonly Transform target;

    private float attackCooldownTimer = 0f;

    public EnemyStrategy(Enemy enemy, Transform target)
    {
        this.enemy = enemy;
        this.target = target;
    }

    public void Tick(float dt)
    {
        attackCooldownTimer -= dt;

        if (target == null)
            return;

        float distance = Vector2.Distance(enemy.transform.position, target.position);

        // Wykrywanie
        if (distance > enemy.Config.detectionRange)
        {
            enemy.StopMoving();
            return;
        }

        // Podejście
        if (distance > enemy.Config.attackRange)
        {
            MoveTowardsTarget(dt);
            return;
        }

        // Atakowanie
        TryAttack();
    }

    private void MoveTowardsTarget(float dt)
    {
        Vector2 dir = (target.position - enemy.transform.position).normalized;
        float moveSpeed = enemy.Stats.GetStat(StatType.MoveSpeed);

        enemy.Move(dir * moveSpeed);
    }

    private void TryAttack()
    {
        if (attackCooldownTimer > 0f)
            return;

        attackCooldownTimer = enemy.Config.attackCooldown;

        // FINAL ATTACK HERE #TODO
        enemy.DealDamage(enemy.Config.damage);
    }
}
