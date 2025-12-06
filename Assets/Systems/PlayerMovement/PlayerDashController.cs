using UnityEngine;

public class PlayerDashController
{
    private float dashDuration = 0.2f;
    private float dashCooldown = 1f;

    private float dashTimer;
    private float lastDashTime = -99f;

    public bool IsDashing { get; private set; }

    public void TryStartDash(Vector2 movementInput)
    {
        if (movementInput == Vector2.zero) return; 
        if (Time.time < lastDashTime + dashCooldown) return;

        IsDashing = true;
        dashTimer = dashDuration;
        lastDashTime = Time.time;
    }

    public void UpdateDashTimer()
    {
        if (!IsDashing) return;

        dashTimer -= Time.deltaTime;
        if (dashTimer <= 0f)
            IsDashing = false;
    }
}
