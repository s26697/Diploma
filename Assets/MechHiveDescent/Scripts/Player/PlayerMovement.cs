using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _dashSpeed = 15f;
    [SerializeField] private float _dashDuration = 0.2f;
    [SerializeField] private float _dashCooldown = 1f;

    private Playeractions controls;
    private Rigidbody2D _rigidbody;

    private Vector2 _movementInput;
    private bool _isDashing = false;
    private float _dashTime = 0f;
    private float _lastDashTime = -10f;

    private void Awake()
    {
        controls = new Playeractions();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable() => controls.Player.Enable();
    private void OnDisable() => controls.Player.Disable();
    private void FixedUpdate()
    {
        ReadInput();
        MovePlayer();
    }

    private void Update()
    {
        // Dash input is checked in Update() for responsiveness
        if (controls.Player.Dash.triggered)
            TryStartDash();
    }

    
    private void ReadInput()
    {
        _movementInput = controls.Player.Move.ReadValue<Vector2>();
    }

    
    private void MovePlayer()
    {
        float currentSpeed = _isDashing ? _dashSpeed : _speed;
        Vector2 move = _movementInput.normalized * currentSpeed * Time.fixedDeltaTime;

        _rigidbody.MovePosition(_rigidbody.position + move);

        if (_isDashing)
        {
            _dashTime -= Time.fixedDeltaTime;
            if (_dashTime <= 0f)
                _isDashing = false;
        }
    }

    
    private void TryStartDash()
    {
        if (_movementInput == Vector2.zero) return; 
        if (Time.time < _lastDashTime + _dashCooldown) return; 

        _isDashing = true;
        _dashTime = _dashDuration;
        _lastDashTime = Time.time;
    }
}
