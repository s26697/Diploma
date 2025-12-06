using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    private Playeractions controls;
    private WeaponSystem weapon;
    private StatOwner stats;
    private bool isFiring = false;

    private PlayerMovementController movementController;
    private PlayerDashController dashController;

    private void Awake()
    {
        InitSingleton();

        controls = new Playeractions();
        stats = GetComponent<StatOwner>();
        weapon = GetComponent<WeaponSystem>();

        movementController = new PlayerMovementController(GetComponent<Rigidbody2D>(), stats);
        dashController = new PlayerDashController();

        BindInputCallbacks();
    }

    private void InitSingleton()
    {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(gameObject);
    }

    private void BindInputCallbacks()
    {
        // DASH INPUT (event-driven)
        controls.Player.Dash.started += _ => dashController.TryStartDash(movementController.MoveInput);

        controls.Player.Fire.performed += _ => isFiring = true;
        controls.Player.Fire.canceled  += _ => isFiring = false;

        // MOVEMENT INPUT
        controls.Player.Move.performed += ctx => 
            movementController.SetMovementInput(ctx.ReadValue<Vector2>());
        controls.Player.Move.canceled += _ => 
            movementController.SetMovementInput(Vector2.zero);

        // DEBUG SPAWN
        controls.Player.Spawn.started += _ => SpawnDebugEnemy();
    }

    private void OnEnable() => controls.Enable();
    private void OnDisable() => controls.Disable();

    private void Update()
    {
        dashController.UpdateDashTimer();
        movementController.UpdateFacing(GetMouseWorldPos());
        HandleContinuousFire();
    }

    private void HandleContinuousFire()
{
    if (!isFiring) return;
    AttackInput();
}


    private void FixedUpdate()
    {
        movementController.Move(dashController.IsDashing);
    }

    // -------------------------------------------------------
    // Helper Methods
    // -------------------------------------------------------

    private Vector3 GetMouseWorldPos()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        pos.z = 0;
        return pos;
    }

    private void AttackInput()
    {
        Vector3 mouse = GetMouseWorldPos();
        Vector2 dir = (mouse - transform.position).normalized;
        weapon.Attack(dir);
    }

    private void SpawnDebugEnemy()
    {
        Vector2 pos = (Vector2)(transform.position + (transform.up * 3f));
        GameEvents.RaiseRequestSpawnEnemy(pos);
    }
}
