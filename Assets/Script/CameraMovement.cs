using UnityEngine;
public class CameraMovement : MonoBehaviour {
    
    public float moveSpeed = 20f;
    public float moveSpeedMouse = 200f;
    public float scrollSpeed = 10f;
    public int maxX = 33;
    public int maxZ = 25;
    public int minX = 0;
    public int minZ = -5;
    public float minFov = 15f;
    public float maxFov = 90f;
    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
    }

    void Update () {
        
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            transform.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")) *
                                  (moveSpeed * Time.deltaTime);
            if (transform.position.x > maxX)
            {
                var position = transform.position;
                position.Set(maxX, position.y, position.z);
                transform.position = position;
            }
            if (transform.position.z > maxZ)
            {
                var position = transform.position;
                position.Set(position.x, position.y, maxZ);
                transform.position = position;
            }
            if (transform.position.x < minX)
            {
                var position = transform.position;
                position.Set(minX, position.y, position.z);
                transform.position = position;
            }
            if (transform.position.z < minZ)
            {
                var position = transform.position;
                position.Set(position.x, position.y, minZ);
                transform.position = position;
            }
        }
        
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            ScrollCamera();
        }

        if (Input.GetMouseButton(1))
        {
            transform.position += new Vector3(-Input.GetAxis("Mouse X"), 0, -Input.GetAxis("Mouse Y")) *
                                  (moveSpeedMouse * Time.deltaTime);
        }
    }

    private void ScrollCamera()
    {
        float fov = _camera.fieldOfView;
        fov += -Input.GetAxis("Mouse ScrollWheel") * scrollSpeed;
        fov = Mathf.Clamp(fov, minFov, maxFov);
        _camera.fieldOfView = fov;
    }
}