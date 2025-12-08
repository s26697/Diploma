using System;
using UnityEngine;

public static class GameEvents
{
    
    // Game flow
    public static event Action OnGameStarted;
    public static event Action OnGamePaused;
    public static event Action OnGameResumed;
    public static Action OnGameWon;
    public static Action OnGameLost;

    // Waves
    public static event Action<int> OnWaveStarted;
    public static event Action<int> OnWaveCompleted;
    public static event Action<int> OnWaveAllEnemiesDefeated;

    // Spawning
    public static event Action<Enemy> OnEnemySpawned;

    public static event Action<Enemy> OnEnemyDied;

    public static event Action<Vector2> RequestSpawnEnemy;

    //player
    public static Action OnPlayerDied;

    // Game flow f
    public static void GameStarted() => OnGameStarted?.Invoke();
    public static void GamePaused() => OnGamePaused?.Invoke();
    public static void GameResumed() => OnGameResumed?.Invoke();
    public static void GameWon() => OnGameWon?.Invoke();
    public static void GameLost() => OnGameLost?.Invoke();

     // Waves f
    public static void WaveStarted(int wave) => OnWaveStarted?.Invoke(wave);
    public static void WaveCompleted(int wave) => OnWaveCompleted?.Invoke(wave);
    public static void WaveAllEnemiesDefeated(int wave) => OnWaveAllEnemiesDefeated?.Invoke(wave);

    
    // Spawning
    public static void EnemySpawned(Enemy enemy) => OnEnemySpawned?.Invoke(enemy);

    public static void EnemyDied(Enemy e)
    {
        OnEnemyDied?.Invoke(e);

        
        if (e != null)
            PlayerGainedXP(e.Config.xpReward); 
    }
    
    public static void RaiseRequestSpawnEnemy(Vector2 position) => RequestSpawnEnemy?.Invoke(position);
    
    //player 
    public static void PlayerDied() => OnPlayerDied?.Invoke();

    public static event Action<float> OnPlayerGainedXP;
    public static void PlayerGainedXP(float amount) => OnPlayerGainedXP?.Invoke(amount);


    // HUD-specific
    public static event Action<int> OnWaveTimerTick;  
    public static event Action<float, float> OnPlayerHealthChanged;

    public static void WaveTimerTick(int seconds) => OnWaveTimerTick?.Invoke(seconds);

    public static void PlayerHealthChanged(float current, float max) =>
        OnPlayerHealthChanged?.Invoke(current, max);

    // pausemenu
    public static event Action OnPauseMenuOpened;
    public static event Action OnPauseMenuClosed;

    public static void PauseMenuOpened() => OnPauseMenuOpened?.Invoke();
    public static void PauseMenuClosed() => OnPauseMenuClosed?.Invoke();

}


