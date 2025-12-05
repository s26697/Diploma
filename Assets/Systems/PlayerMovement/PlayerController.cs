using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{

    public static PlayerController Instance;

    private StatOwner _statOwner;

    [Header("Movement Settings")]
    
    //[SerializeField] private float _dashSpeed = 15f; // #TODO dodać do statsystemu, i cant be bothered atm
    [SerializeField] private float _dashDuration = 0.2f;
    [SerializeField] private float _dashCooldown = 1f;

    public SpriteRenderer mySpriteRender;

    private Playeractions controls;
    private Rigidbody2D _rigidbody;

    private WeaponSystem _weaponSystem;

    private Vector2 _movementInput;
    private bool _isDashing = false;
    private float _dashTime = 0f;
    private float _lastDashTime = -10f;
    

    private void Awake()
    {
        if ( Instance == null) {
            Instance = this;
        }else if (Instance != this){
            Destroy(this);
        }

        controls = new Playeractions();
        _rigidbody = GetComponent<Rigidbody2D>();
        _statOwner = GetComponent<StatOwner>();
        _weaponSystem = GetComponent<WeaponSystem>();
    }

    private void OnEnable() => controls.Player.Enable();
    private void OnDisable() => controls.Player.Disable();
    private void FixedUpdate()
    {
        ReadInput();
        MovePlayer();
        AdjustPlayerFacingDirection();
    }

    private void Update()
    {
        // Dash input is checked in Update() for responsiveness
        if (controls.Player.Dash.triggered)
            TryStartDash();

             
        // Continuous attack input
    if (controls.Player.Fire.ReadValue<float>() > 0f) // przytrzymanie przycisku
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        mouseWorldPos.z = 0f;
        Vector2 direction = (mouseWorldPos - transform.position).normalized;

        _weaponSystem.Attack(direction);
    }

    // Spawn enemy on input
    if (controls.Player.Spawn.triggered) //#todo 
    {
        Vector2 pos = transform.position + transform.up * 3f; 
        GameEvents.RaiseRequestSpawnEnemy(pos);
    }
    }

    
    private void ReadInput()
    {
        _movementInput = controls.Player.Move.ReadValue<Vector2>();
    }

    
    private void MovePlayer()
    {
        var _speed =_statOwner.GetStat(StatType.MoveSpeed);
        
        float currentSpeed = _isDashing ? _speed * 3 : _speed; //#todo może custom dashspeed czy coś na razie hardcode. idc
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

     private void AdjustPlayerFacingDirection()
    {
    
    Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
    mouseWorldPos.z = 0f;

    Vector3 scale = transform.localScale;

    if (mouseWorldPos.x < transform.position.x)
        scale.x = -Mathf.Abs(scale.x); 
    else
        scale.x = Mathf.Abs(scale.x);  

    transform.localScale = scale;
    }


}
