using UnityEngine;

[CreateAssetMenu(fileName = "EnemyConfig", menuName = "Enemies/Enemy Config")]
public class EnemyConfigSO : ScriptableObject
{
    [Header("Prefab")]
    public Enemy prefab;

     public float xpReward = 5;

    [Header("AI")]
    public float detectionRange = 6f;
    public float attackRange = 1.3f;
    public float attackCooldown = 1f;

    [Header("Damage")]
    public float damage = 2f;

    [Header("Stats (loaded into StatOwner on spawn)")]
    public StatDefinitionSO[] startingStats;
}
