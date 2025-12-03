using UnityEngine;
using UnityEngine.InputSystem;

public class RotateGun : MonoBehaviour
{
    void Update()
    {
        Rotate();
    }

    private void Rotate()
    {
        
        Vector2 mouseScreenPos = Mouse.current.position.ReadValue();

        
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
        mouseWorldPos.z = 0f; 

        
        Vector2 direction = PlayerController.Instance.transform.InverseTransformPoint(mouseWorldPos);

       
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.localRotation = Quaternion.Euler(0, 0, angle);
    }
}
