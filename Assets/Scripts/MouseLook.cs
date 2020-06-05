
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;         // Mouse Sensitivity 
    public Transform playerBody;                  // Get Player Body Transform
    public float smoothAmount = 400f;             // Camera Smoothness

    float xRotation = 0f;

    bool canLook;

    float yaw;
    float pitch;
    float mouseX;
    float mouseY;
    
    void Start()
    {
        canLook = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        transform.localRotation = Quaternion.identity;
    }

    void Update()
    {
        if (!canLook)
            return;
    
        yaw = Input.GetAxis("Mouse X");
        pitch = Input.GetAxis("Mouse Y");

        mouseX = yaw * mouseSensitivity * Time.deltaTime;
        mouseY = pitch * mouseSensitivity * Time.deltaTime;

    }

   
    void LateUpdate()
    {
        if (!canLook)
            return;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90, 90);

        yaw = Mathf.Lerp(yaw, xRotation, smoothAmount * Time.deltaTime);
        pitch = Mathf.Lerp(pitch, mouseX, smoothAmount * Time.deltaTime);

        transform.localRotation = Quaternion.Euler(yaw, 0f, 0f);        // Rotate Player Head (Camera) Up and Down
        playerBody.Rotate(Vector3.up * pitch);                          // Rotate Player Body (Capsule) Left and Right

    }

    public void DisableLook()
    {
        canLook = false;
    }

    public void EnableLook()
    {
        canLook = true;
    }

}
