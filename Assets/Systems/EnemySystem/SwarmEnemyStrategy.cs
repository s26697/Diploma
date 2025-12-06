using UnityEngine;

public class SwarmEnemyStrategy : IEnemyStrategy
{
    private readonly Enemy _enemy;
    private readonly Transform _target;

    public SwarmEnemyStrategy(Enemy enemy, Transform target)
    {
        _enemy = enemy;
        _target = target;
    }

    public void Tick(float dt)
    {
        if (_target == null)
        {
            _enemy.StopMoving();
            return;
        }

        Vector2 direction = (_target.position - _enemy.transform.position).normalized;
        float speed = _enemy.Stats.GetStat(StatType.MoveSpeed);

        _enemy.Move(direction * speed);
    }
}
