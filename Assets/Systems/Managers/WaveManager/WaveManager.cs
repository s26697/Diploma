using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Systems.Managers.WaveManager;

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
            minDistanceFromPlayer,
            this
        );
        
        InitializeIndicatorPools();
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
            SetupWave(i);
            yield return StartCoroutine(spawnStrategy.ExecuteSpawn(waves[i]));
            yield return new WaitUntil(() => WaveCompletedCondition());
            CompleteWave(i);
        }
    }

    private void SetupWave(int index)
    {
        currentWave = index;
        aliveEnemies = 0;
        waveTimeExpired = false;
        waveTimer = waves[index].timeToCOmplete;
        GameEvents.WaveStarted(index);
    }

    private bool WaveCompletedCondition()
    {
        return aliveEnemies == 0 || waveTimeExpired;
    }

    private void CompleteWave(int index)
    {
        GameEvents.WaveCompleted(index);
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
    private void InitializeIndicatorPools()
    {
        foreach (var wave in waves)
        {
            foreach (var entry in wave.enemies)
            {
                var indicator = entry.config.spawnIndicator;
                indicator?.InitPool(transform);
            }
        }
    }
    
    public IEnumerator ShowIndicatorBatch(List<IEnumerator> indicatorCoroutines)
    {
        if (indicatorCoroutines == null || indicatorCoroutines.Count == 0)
            yield break;
        
        List<Coroutine> running = new List<Coroutine>();

        foreach (var c in indicatorCoroutines)
        {
            running.Add(StartCoroutine(c));
        }
        
        foreach (var r in running)
            yield return r;
    }
}
