using System;
using UnityEngine;

public static class GameEvents
{
    public static event Action<Vector2> RequestSpawnEnemy;

    public static void RaiseRequestSpawnEnemy(Vector2 position)
    {
        RequestSpawnEnemy?.Invoke(position);
    }
}
