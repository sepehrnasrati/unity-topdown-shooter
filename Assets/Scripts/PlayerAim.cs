using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    public Camera cam;             // Camera reference
    public Transform firePoint;    // Only this rotates!
    public SpriteRenderer sr;      // Player sprite renderer (for flipping)

    private Vector2 mouseWorldPos;

    void Update()
    {
        // Convert mouse position to world position
        mouseWorldPos = cam.ScreenToWorldPoint(Input.mousePosition);

        // Aim direction (player -> mouse)
        Vector2 aimDir = (mouseWorldPos - (Vector2)transform.position).normalized;

        // Rotate firePoint ONLY
        float angle = Mathf.Atan2(aimDir.y, aimDir.x) * Mathf.Rad2Deg;
        firePoint.rotation = Quaternion.Euler(0, 0, angle - 90f);

        // ---- OPTIONAL: Flip sprite left/right ----
        if (sr != null)
        {
            if (mouseWorldPos.x < transform.position.x)
                sr.flipX = true;       // face left
            else
                sr.flipX = false;      // face right
        }
    }
}
