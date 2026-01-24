using UnityEngine;

public class MouseCrosshair : MonoBehaviour
{
    public Camera cam;

    void Start()
    {
        // hide the system cursor
        Cursor.visible = false;
    }

    void Update()
    {
        Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f; // make sure its on the 2D plane
        transform.position = mousePos;
    }

    private void OnDisable()
    {
        // show cursor again if this object is disabled
        Cursor.visible = true;
    }
}
