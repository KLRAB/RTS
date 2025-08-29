using UnityEngine;

public class FreeCamera : MonoBehaviour
{
    public float moveSpeed = 15f;
    public float fastMult = 2f;
    public float rotateSpeed = 100f;
    public float zoomSpeed = 50f;
    public float minY = 8f, maxY = 60f;

    void Update()
    {
        float mult = Input.GetKey(KeyCode.LeftShift) ? fastMult : 1f;
        Vector3 dir = Vector3.zero;
        if (Input.GetKey(KeyCode.W)) dir += transform.forward;
        if (Input.GetKey(KeyCode.S)) dir -= transform.forward;
        if (Input.GetKey(KeyCode.A)) dir -= transform.right;
        if (Input.GetKey(KeyCode.D)) dir += transform.right;

        dir.y = 0; // p³asko
        transform.position += dir.normalized * moveSpeed * mult * Time.deltaTime;

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (Mathf.Abs(scroll) > 0.0001f)
        {
            var p = transform.position;
            p += transform.forward * (scroll * zoomSpeed);
            p.y = Mathf.Clamp(p.y, minY, maxY);
            transform.position = p;
        }

        if (Input.GetMouseButton(2)) // œrodkowy przycisk
        {
            float yaw = Input.GetAxis("Mouse X") * rotateSpeed * Time.deltaTime;
            transform.Rotate(Vector3.up, yaw, Space.World);
        }
    }
}
