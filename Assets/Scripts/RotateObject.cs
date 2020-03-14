
using UnityEngine;



public class RotateObject : MonoBehaviour
{
    public float rotationSpeed = 10f;              // The rotation speed

    public bool canRotateX;                        // Is rotation allowed on Mouse X axis

    public bool canRotateY;                        // Is rotation allowed on Mouse Y axis

    public Transform objectParent;                 // Transform for objects parent so that position can be restored


    [HideInInspector] public bool canRotate;

  

    void Start()
    {

        canRotate = false;
    }

    void Update()
    {

        if(canRotate)
        {
            if(canRotateX)
            {
                float xRotation = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
                transform.Rotate(Vector3.down, xRotation);

            }

            if(canRotateY)
            {
                float yRotation = Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;
                transform.Rotate(Vector3.right, yRotation);
            }
            
        }   
    }
    
}
