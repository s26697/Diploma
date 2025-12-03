using UnityEngine;

public class WeaponSystem : MonoBehaviour
{
    [SerializeField] private WeaponConfigSO _weaponConfig;
    [SerializeField] private Transform _muzzle; // punkt spawnu pocisków

    private Weapon _weapon;
    private ProjectileFactory _factory;
    private IStatOwner _stats;

    private void Awake()
    {
        _stats = GetComponent<IStatOwner>();  
        _factory = new ProjectileFactory();
        _weapon = new Weapon(_weaponConfig, _factory, _stats);
    }

    private void Update()
    {
        _weapon.Tick(Time.deltaTime);
    }

    public void Attack(Vector2 direction)
    {
        _weapon.Attack(_muzzle.position, direction);
    }
}
