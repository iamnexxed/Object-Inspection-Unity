// Create Game Object with Tag Obstacle and a rigidbody component where jump glitch seems to happen

using UnityEngine;

public class DetectObstacle : MonoBehaviour
{
   public float avoidGlitchJumpHeight = 0.5f;

    void OnTriggerStay(Collider other)
    {
        // The Object should have an Obstacle Tag as well as a rigidbody to it
        if (other.CompareTag("Obstacle") && !PlayerMovement.instance.isGrounded)
        {
            // Debug.Log("Obstacle Detected");
            // PlayerMovement.instance.canMove = false;
           
            PlayerMovement.instance.characterController.Move(transform.up * avoidGlitchJumpHeight);
        }
        else
        {
            // Debug.Log("The collider collided with : " + other.name);
        }

    }
    void OnTriggerExit(Collider other) 
    {
        if(other.CompareTag("Obstacle"))
        {
            Debug.Log("Leaving Obstacle");
            // PlayerMovement.instance.canMove = true;
        }
    }
}
