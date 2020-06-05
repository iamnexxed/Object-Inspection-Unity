
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;                     // To access the Player Movement Parameters from other scripts

    [HideInInspector] public bool canMove;                     // Is the player Allowed to Move?

    public float speed = 5f;                                   // Control the Speed of Player
    
    public float gravity = -9.81f;                             // Gravity of Earth
    public float jumpHeight = 3f;
    public Transform groundCheck;                              // Tranform for getting the feet of player to check whether player is touching the ground
    public float groundDistance = 0.2f;                        // Height of Feet
    public LayerMask groundMask;                               // Check for which layers in the game are Ground

    CharacterController characterController;

    Vector3 velocity;

    [HideInInspector]
    public bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        // Important for the initialization of instance of an object in any new Scene
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        characterController = GetComponent<CharacterController>();
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!canMove)
            return;

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        characterController.Move(move * speed * Time.deltaTime);

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * (-2f) * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);    // dy = g * t *t
      
    }
}
