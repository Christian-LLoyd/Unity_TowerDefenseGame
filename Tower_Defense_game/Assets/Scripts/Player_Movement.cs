using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 10f; 
    public float rotationSpeed = 10f; // Speed at which the player rotates to face the movement direction

    private Rigidbody rb;
    private Vector3 moveDirection;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; 
        rb.isKinematic = false; 
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic; 
        rb.interpolation = RigidbodyInterpolation.Interpolate; 
    }

    void Update()
    {
        HandleMovementInput();
    }

    void FixedUpdate()
    {
        MovePlayer();
        RotatePlayer();
    }

    void HandleMovementInput()
    {
        float moveX = Input.GetAxisRaw("Horizontal"); 
        float moveZ = Input.GetAxisRaw("Vertical");  

        // Set movement direction (XZ plane, Y remains unchanged)
        moveDirection = new Vector3(-moveZ, 0, moveX).normalized; 
    }

    void MovePlayer()
    {
        // Apply movement without multiplying by Time.fixedDeltaTime
        Vector3 moveVelocity = new Vector3(moveDirection.x * moveSpeed, rb.linearVelocity.y, moveDirection.z * moveSpeed);
        rb.linearVelocity = moveVelocity;
    }

    void RotatePlayer()
    {
        if (moveDirection != Vector3.zero)
        {
            // Rotate the player to face the movement direction
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
        }
    }
}