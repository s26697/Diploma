using UnityEngine;

public class PlayerMovementController
{
    private readonly Rigidbody2D rb;
    private readonly StatOwner stats;

    public Vector2 MoveInput { get; private set; }

    public PlayerMovementController(Rigidbody2D rb, StatOwner stats)
    {
        this.rb = rb;
        this.stats = stats;
    }

    public void SetMovementInput(Vector2 input)
    {
        MoveInput = input;
    }

    private float GetCurrentSpeed(bool isDashing)
    {
        float baseSpeed = stats.GetStat(StatType.MoveSpeed);
        return isDashing ? baseSpeed * 3f : baseSpeed; // Twój hardcode
    }

    public void Move(bool isDashing)
    {
        float speed = GetCurrentSpeed(isDashing);
        Vector2 move = MoveInput.normalized * speed * Time.fixedDeltaTime;

        rb.MovePosition(rb.position + move);
    }

    public void UpdateFacing(Vector3 mouseWorld)
    {
        Transform t = rb.transform;

        // preferuj flipX jeśli masz SpriteRenderer
        SpriteRenderer sr = t.GetComponentInChildren<SpriteRenderer>();
        if (sr != null)
        {
            sr.flipX = mouseWorld.x < t.position.x;
            return;
        }

        // fallback: scale flip
        Vector3 scale = t.localScale;
        scale.x = mouseWorld.x < t.position.x ? -Mathf.Abs(scale.x) : Mathf.Abs(scale.x);
        t.localScale = scale;
    }
}
