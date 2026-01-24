using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraFollow : MonoBehaviour
{
    [Header("Follow Settings")]
    public Transform target;     
    public float smoothSpeed = 5f;
    public Vector3 offset;

    [Header("Tilemap for Auto Bounds")]
    public Tilemap tilemap;      

    private Camera cam;
    private float halfHeight;
    private float halfWidth;

    private Vector2 minBounds;
    private Vector2 maxBounds;

    void Start()
    {
        cam = GetComponent<Camera>();

        // Camera size values
        halfHeight = cam.orthographicSize;
        halfWidth = halfHeight * cam.aspect;

        // auto-calc bounds from tilemap
        if (tilemap != null)
        {
            Bounds tmBounds = tilemap.localBounds;

            minBounds = tmBounds.min;
            maxBounds = tmBounds.max;
        }
        else
        {
            Debug.LogWarning("CameraFollow: No tilemap assigned! Bounds will not be clamped.");
        }
    }

    void LateUpdate()
    {
        if (target == null) return;

        // smooth follow
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothPos = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        
        float clampedX = Mathf.Clamp(smoothPos.x, minBounds.x + halfWidth, maxBounds.x - halfWidth);
        float clampedY = Mathf.Clamp(smoothPos.y, minBounds.y + halfHeight, maxBounds.y - halfHeight);

        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }
}
