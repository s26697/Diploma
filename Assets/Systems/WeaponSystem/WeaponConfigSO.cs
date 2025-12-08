using UnityEngine;

[CreateAssetMenu(menuName = "Game/Weapon Config")]
public class WeaponConfigSO : ScriptableObject
{
    [Header("Visuals")]
    //public Sprite weaponSprite;
    //public RuntimeAnimatorController animatorController;

    [Header("Base Stats")]
    public float baseAttackSpeed = 1f; 
    public int baseProjectiles = 1;  
    public float baseAccuracy = 1f;      

    [Header("Projectile")]
    public ProjectileConfigSO projectileConfig;
}
