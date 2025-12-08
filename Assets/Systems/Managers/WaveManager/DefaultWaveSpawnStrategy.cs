using UnityEngine;
using System.Collections;

public class DefaultWaveSpawnStrategy : IWaveSpawnStrategy
{
    private readonly EnemyFactory factory;
    private readonly Transform player;
    private readonly Vector2 mapMin;
    private readonly Vector2 mapMax;
    private readonly float minDistanceFromPlayer;
    private readonly System.Action onEnemySpawned;

    public DefaultWaveSpawnStrategy(
        EnemyFactory factory,
        Transform player,
        Vector2 mapMin,
        Vector2 mapMax,
        float minDistanceFromPlayer)
    {
        this.factory = factory;
        this.player = player;
        this.mapMin = mapMin;
        this.mapMax = mapMax;
        this.minDistanceFromPlayer = minDistanceFromPlayer;
    }

    public IEnumerator ExecuteSpawn(WaveSO wave)
    {
        foreach (var entry in wave.enemies)
        {
            for (int i = 0; i < entry.count; i++)
            {
                Vector2 spawnPos = GetRandomSpawnPosition();
                factory.Spawn(entry.config, spawnPos, player);

                GameEvents.EnemySpawned(entry.config.prefab);

                yield return new WaitForSeconds(wave.spawnInterval);
            }
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
