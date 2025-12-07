using UnityEngine;
using System.Collections;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private WaveSO[] waves;
    [SerializeField] private Transform player;
    

    [Header("map range and spawning settings")]
    [SerializeField] private Vector2 mapMin;  
    [SerializeField] private Vector2 mapMax;  
    [SerializeField] private float minDistanceFromPlayer = 5f;

    public int TotalWaves => waves.Length;

    private int poolSize = 30;

    private EnemyFactory factory;
    private int currentWave = -1;
    private int aliveEnemies = 0;

    private void Awake()
    {
        factory = new EnemyFactory(poolSize, transform);
    }

    private void OnEnable()
    {
        GameEvents.OnGameStarted += StartWaveFlow;
        GameEvents.OnEnemyDied += OnEnemyDied;
    }

    private void OnDisable()
    {
        GameEvents.OnGameStarted -= StartWaveFlow;
        GameEvents.OnEnemyDied -= OnEnemyDied;
    }

    private void StartWaveFlow()
    {
        StartCoroutine(WaveRoutine());
    }

    private IEnumerator WaveRoutine()
    {
        for (int i = 0; i < waves.Length; i++)
        {
            currentWave = i;
            aliveEnemies = 0;

            var wave = waves[i];
            GameEvents.WaveStarted(i);

            yield return StartCoroutine(SpawnWave(wave));

            if (wave.waitForClear)
                yield return new WaitUntil(() => aliveEnemies == 0);

            GameEvents.WaveCompleted(i);
        }
    }

    private IEnumerator SpawnWave(WaveSO wave)
    {
        foreach (var entry in wave.enemies)
        {
            for (int i = 0; i < entry.count; i++)
            {
                
                
                factory.Spawn(entry.config, GetRandomSpawnPosition(), player);
                aliveEnemies++;

                yield return new WaitForSeconds(wave.spawnInterval);
            }
        }
    }

    private void OnEnemyDied(Enemy e)
    {
        aliveEnemies--;
    }

    private Vector2 GetRandomSpawnPosition()
{
    Vector2 playerPos = player.position;

    for (int i = 0; i < 20; i++)
    {
        float x = Random.Range(mapMin.x, mapMax.x);
        float y = Random.Range(mapMin.y, mapMax.y);

        Vector2 candidate = new Vector2(x, y);

        if ((candidate - playerPos).sqrMagnitude >= minDistanceFromPlayer * minDistanceFromPlayer)
            return candidate;
    }

    return playerPos + (Random.insideUnitCircle.normalized * minDistanceFromPlayer);
}

}
