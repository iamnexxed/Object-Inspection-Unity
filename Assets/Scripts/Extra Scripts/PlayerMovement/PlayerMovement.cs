
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;                     // To access the Player Movement Parameters from other scripts

    [HideInInspector] public bool canMove;                     // Is the player Allowed to Move?

    public float speed = 5f;                                   // Control the Speed of Player
    
    public float gravity = -9.81f;                             // Gravity of Earth
    public float jumpHeight = 3f;
    public float jumpInterval = 1f;
    public Transform groundCheck;                              // Tranform for getting the feet of player to check whether player is touching the ground
    public float groundDistance = 0.2f;                        // Height of Feet
    public LayerMask groundMask;                               // Check for which layers in the game are Ground

    public CharacterController characterController;

    [HideInInspector] public Vector3 velocity;

    [HideInInspector] public bool isGrounded;

    float timer = 0;

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

        canMove = true;
    }

    void FixedUpdate()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (canMove)
        {

            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }

            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            velocity.x = x * speed;
            velocity.z = z * speed;

            Vector3 move = transform.right * x + transform.forward * z;
            characterController.Move(move * speed * Time.deltaTime);

            if (Input.GetButtonDown("Jump") && isGrounded && (timer >= jumpInterval))
            {
                velocity.y = Mathf.Sqrt(jumpHeight * (-2f) * gravity);
                timer = 0;
            }
        }


        velocity.y += gravity * Time.deltaTime;
        Vector3 verticalVelocity = new Vector3(0, velocity.y, 0);
        characterController.Move(verticalVelocity * Time.deltaTime);    // dy = g * t * t
        // Debug.Log("The Velocity variable is : " + velocity);

        // Debug.Log("Player is grounded: " + isGrounded);
    }

}
