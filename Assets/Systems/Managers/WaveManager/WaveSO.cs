using UnityEngine;

[CreateAssetMenu(menuName = "Game/Wave")]
public class WaveSO : ScriptableObject
{
    public WaveEntry[] enemies;
    public float spawnInterval = 0.2f;
    public float timeToCOmplete = 20;
    public bool waitForClear = true;
}

[System.Serializable]
public class WaveEntry
{
    public EnemyConfigSO config;
    public int count;
}
