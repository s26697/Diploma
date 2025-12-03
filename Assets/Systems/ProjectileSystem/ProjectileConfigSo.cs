using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileConfig", menuName = "Weapons/Projectile")]
public class ProjectileConfigSO : ScriptableObject
{
    [Header("Prefab")]
    
    public Projectile prefab;

    [Header("Stats")]
    public float speed = 10f;
    public float lifetime = 2f;
    public float damage = 10f;
    public float maxDistance = 20f;

    [Header("Physics")]
    public bool rotateToVelocity = true;
}
