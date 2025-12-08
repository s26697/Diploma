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


    private EnemyFactory factory;
    private IWaveSpawnStrategy spawnStrategy;

    private int currentWave = -1;
    private int aliveEnemies = 0;

    private float waveTimer = 0f;
    private bool waveTimeExpired = false;

    private void Awake()
    {
        factory = new EnemyFactory(30, transform);

        spawnStrategy = new DefaultWaveSpawnStrategy(
            factory,
            player,
            mapMin,
            mapMax,
            minDistanceFromPlayer
        );
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

    private void Update()
    {
        WaveTimerTick();
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
            waveTimeExpired = false;

            var wave = waves[i];
            waveTimer = wave.timeToCOmplete;

            GameEvents.WaveStarted(i);

            
            yield return StartCoroutine(spawnStrategy.ExecuteSpawn(wave));

            
            yield return new WaitUntil(() =>
                aliveEnemies == 0 || waveTimeExpired
            );

            GameEvents.WaveCompleted(i);
        }
    }

    private void WaveTimerTick()
    {
        if (currentWave < 0) return;

        waveTimer -= Time.deltaTime;

        if (waveTimer <= 0f)
        {
            waveTimer = 0f;
            waveTimeExpired = true;
        }

        GameEvents.WaveTimerTick(Mathf.CeilToInt(waveTimer));
    }

    private void OnEnemyDied(Enemy e)
    {
        aliveEnemies--;
    }
}
