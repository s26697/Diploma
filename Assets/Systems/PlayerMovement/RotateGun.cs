using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(SpriteRenderer))]
public class RotateGun : MonoBehaviour
{
    [SerializeField] private Transform muzzle; // punkt wylotu pocisku

    private SpriteRenderer playerSpriteRenderer;
    private Vector3 originalScale;
    private Camera mainCamera;
    private Transform playerTransform;

    private void Start()
    {
        if (muzzle == null)
            Debug.LogWarning("RotateGun: Muzzle not assigned!");

        playerSpriteRenderer = PlayerController.Instance.sr;
        playerTransform = PlayerController.Instance.transform;
        mainCamera = Camera.main;
        originalScale = transform.localScale;
    }

    private void Update()
    {
        Rotate();
        ApplyFlip();
    }

    private void Rotate()
    {
        if (muzzle == null) return;

        
        Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
        Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(mouseScreenPos);
        mouseWorldPos.z = 0f;

        
        Vector2 direction = mouseWorldPos - muzzle.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.localRotation = Quaternion.Euler(0, 0, angle);
    }

    private void ApplyFlip()
    {
        if (playerSpriteRenderer.flipX)
            transform.localScale = new Vector3(originalScale.x, -originalScale.y, originalScale.z);
        else
            transform.localScale = originalScale;
    }
}
