using UnityEngine;

public class TestEnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemyConfigSO config;
    [SerializeField] private Transform player;

    private EnemyFactory factory;

    private void Awake()
    {
        factory = new EnemyFactory(config, 5, transform);
    }

    private void OnEnable()
    {
        GameEvents.RequestSpawnEnemy += OnSpawnEnemyRequested;
    }

    private void OnDisable()
    {
        GameEvents.RequestSpawnEnemy -= OnSpawnEnemyRequested;
    }

    private void OnSpawnEnemyRequested(Vector2 position)
    {
        factory.Spawn(config, position, player);
    }
}
