public interface IEnemyStrategy
{
    void Tick(in EnemyContext ctx, float dt);
}
