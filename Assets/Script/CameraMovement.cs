using UnityEngine;
public class CameraMovement : MonoBehaviour {
    
    private float moveSpeed = 20f;
    private float moveSpeedMouse = 200f;
    private float scrollSpeed = 5000f;
    public int maxX;
    public int maxZ;
    public int minX;
    public int minZ;

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
            scrollCamera();
        }

        if (Input.GetMouseButton(1))
        {
            transform.position += new Vector3(-Input.GetAxis("Mouse X"), 0, -Input.GetAxis("Mouse Y")) *
                                  (moveSpeedMouse * Time.deltaTime);
        }
    }

    private void scrollCamera()
    {
        float currentX = transform.forward.x;
        float currentZ = transform.forward.z;
        
        transform.position += new Vector3(-Input.GetAxis("Mouse ScrollWheel") * (currentX / (currentX + currentZ)), -Input.GetAxis("Mouse ScrollWheel"), -Input.GetAxis("Mouse ScrollWheel") * (currentZ / (currentX + currentZ))) * (scrollSpeed * Time.deltaTime);
    }
}