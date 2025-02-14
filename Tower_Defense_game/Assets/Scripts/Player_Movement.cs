using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 3f;  // Adjusted speed for smoother movement
    public float rotationSpeed = 10f; 

    private Rigidbody rb;
    private Vector3 moveDirection;
    private Animator animator; // Animator reference

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>(); // Get Animator component

        if (rb == null) Debug.LogError("Rigidbody is missing on " + gameObject.name);
        if (animator == null) Debug.LogError("Animator is missing on " + gameObject.name);

        rb.freezeRotation = true; 
        rb.isKinematic = false; 
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic; 
        rb.interpolation = RigidbodyInterpolation.Interpolate; 
        
        // Prevents physics from making the player move on its own
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionY;
    }

    void Update()
    {
        HandleMovementInput();
        HandleAnimations(); // Updates Idle & Walk animations
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

        moveDirection = new Vector3(-moveZ, 0, moveX);

        if (moveDirection.sqrMagnitude < 0.01f) // Ensures zero movement is properly detected
        {
            moveDirection = Vector3.zero;
        }
        else
        {
            moveDirection.Normalize();
        }
    }

    void MovePlayer()
    {
        if (moveDirection != Vector3.zero)
        {
            rb.linearVelocity = new Vector3(moveDirection.x * moveSpeed, rb.linearVelocity.y, moveDirection.z * moveSpeed);
        }
        else
        {
            // Smooth stopping to prevent sudden snapping or sliding
            rb.linearVelocity = Vector3.Lerp(rb.linearVelocity, Vector3.zero, Time.fixedDeltaTime * 10f);
        }
    }

    void RotatePlayer()
    {
        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
        }
    }

    void HandleAnimations()
    {
        bool isMoving = moveDirection.sqrMagnitude > 0.01f; // Prevents floating-point errors
        animator.SetBool("isWalking", isMoving);
    }
}
