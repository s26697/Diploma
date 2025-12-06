using UnityEngine;

public class SwarmEnemyStrategy : IEnemyStrategy
{
    private readonly Enemy enemy;
    private readonly Transform target;

    public SwarmEnemyStrategy(Enemy enemy, Transform target)
    {
        this.enemy = enemy;
        this.target = target;
    }

    public void Tick(float dt)
    {
        if (target == null)
        {
            enemy.StopMoving();
            return;
        }

        Vector2 dir = (target.position - enemy.transform.position).normalized;
        float moveSpeed = enemy.Stats.GetStat(StatType.MoveSpeed);

        enemy.Move(dir * moveSpeed);
    }
}
