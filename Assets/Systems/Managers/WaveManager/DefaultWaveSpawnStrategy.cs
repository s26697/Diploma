using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DefaultWaveSpawnStrategy : IWaveSpawnStrategy
{
    private readonly EnemyFactory factory;
    private readonly Transform player;
    private readonly Vector2 mapMin;
    private readonly Vector2 mapMax;
    private readonly float minDistanceFromPlayer;
    private readonly System.Action onEnemySpawned;
    private readonly WaveManager waveManager;

    public DefaultWaveSpawnStrategy(
        EnemyFactory factory,
        Transform player,
        Vector2 mapMin,
        Vector2 mapMax,
        float minDistanceFromPlayer,
        WaveManager waveManager)
    {
        this.factory = factory;
        this.player = player;
        this.mapMin = mapMin;
        this.mapMax = mapMax;
        this.minDistanceFromPlayer = minDistanceFromPlayer;
        this.waveManager = waveManager;
    }

    public IEnumerator ExecuteSpawn(WaveSO wave)
    {
        foreach (var entry in wave.enemies)
        {
            Vector2[] spawnPositions = GenerateBatchPositions(entry.count);
            var indicatorCoroutines = PrepareIndicatorCoroutines(entry.config, spawnPositions);
            if (indicatorCoroutines.Count > 0) yield return waveManager.StartCoroutine(waveManager.ShowIndicatorBatch(indicatorCoroutines));
            SpawnBatch(entry.config, spawnPositions);
            
            yield return new WaitForSeconds(wave.spawnInterval);
        }
    }
    
    private Vector2[] GenerateBatchPositions(int count)
    {
        var positions = new Vector2[count];
        for (int i = 0; i < count; i++)
            positions[i] = GetRandomSpawnPosition();
        return positions;
    }

    private List<IEnumerator> PrepareIndicatorCoroutines(EnemyConfigSO config, Vector2[] positions)
    {
        var list = new List<IEnumerator>();
        if (config.spawnIndicator is not null)
            foreach (var pos in positions)
                list.Add(config.spawnIndicator.Show(pos));
        return list;
    }
    
    private void SpawnBatch(EnemyConfigSO config, Vector2[] positions)
    {
        foreach (var pos in positions)
        {
            factory.Spawn(config, pos, player);
            GameEvents.EnemySpawned(config.prefab);
        }
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
