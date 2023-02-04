using UnityEngine;
public class CameraMovement : MonoBehaviour {
    
    private float moveSpeed = 20f;
    private float moveSpeedMouse = 200f;
    private float scrollSpeed = 5000f;

    void Update () {
        
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            transform.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")) *
                                  (moveSpeed * Time.deltaTime);
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