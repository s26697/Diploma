using UnityEngine;

[RequireComponent(typeof(StatOwner))]
public class WeaponSystem : MonoBehaviour
{
    [SerializeField] private WeaponConfigSO _weaponConfig;
    [SerializeField] private Transform _muzzle;

    private Weapon _weapon;
    private ProjectileFactory _factory;
    private IStatOwner _stats;

    private IDamaging _source;

    private void Awake()
    {
        _stats = GetComponent<IStatOwner>();
        _source = GetComponent<PlayerHealth>();
        _factory = new ProjectileFactory();

        _weapon = new Weapon(_weaponConfig, _factory, _stats, _source);

        if (_muzzle == null)
            Debug.LogWarning($"[{nameof(WeaponSystem)}] Muzzle is missing!");
    }

    private void Update()
    {
        _weapon.Tick(Time.deltaTime);
    }

    public void TryAttack(Vector2 direction)
    {
        if (_muzzle == null)
            return;

        _weapon.Attack(_muzzle.position, direction);
    }
}
