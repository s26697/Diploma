using System;
using UnityEngine;

public static class GameEvents
{
    
    // Game flow
    public static event Action OnGameStarted;
    public static event Action OnGamePaused;
    public static event Action OnGameResumed;
    public static event Action OnGameOver;

    // Waves
    public static event Action<int> OnWaveStarted;
    public static event Action<int> OnWaveCompleted;
    public static event Action<int> OnWaveAllEnemiesDefeated;

    // Spawning
    public static event Action<GameObject> OnEnemySpawned;

    public static event Action<Enemy> OnEnemyDied;

    public static event Action<Vector2> RequestSpawnEnemy;

    // Game flow f
    public static void GameStarted() => OnGameStarted?.Invoke();
    public static void GamePaused() => OnGamePaused?.Invoke();
    public static void GameResumed() => OnGameResumed?.Invoke();
    public static void GameOver() => OnGameOver?.Invoke();

     // Waves f
    public static void WaveStarted(int wave) => OnWaveStarted?.Invoke(wave);
    public static void WaveCompleted(int wave) => OnWaveCompleted?.Invoke(wave);
    public static void WaveAllEnemiesDefeated(int wave) => OnWaveAllEnemiesDefeated?.Invoke(wave);

    
    // Spawning
    public static void EnemySpawned(GameObject enemy) => OnEnemySpawned?.Invoke(enemy);

    public static void EnemyDied(Enemy e) => OnEnemyDied?.Invoke(e);
    
    public static void RaiseRequestSpawnEnemy(Vector2 position) => RequestSpawnEnemy?.Invoke(position);
    
}
