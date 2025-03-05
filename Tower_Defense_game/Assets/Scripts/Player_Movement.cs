using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float rotationSpeed = 10f;

    private Rigidbody rb;
    public Player_Bullet gun;
    private Vector3 moveDirection;
    private Animator animator;

    // Dash Variables
    public float dashDistance = 5f; // Fixed distance dash
    public float dashCooldown = 1f;
    private bool isDashing;
    private float dashCooldownTimer;
    private Vector3 dashTarget;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        if (!rb) Debug.LogError("Rigidbody is missing on " + gameObject.name);
        if (!animator) Debug.LogError("Animator is missing on " + gameObject.name);

        rb.freezeRotation = true;
        rb.isKinematic = false;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        rb.interpolation = RigidbodyInterpolation.Interpolate;

        rb.constraints = RigidbodyConstraints.FreezeRotationX | 
                         RigidbodyConstraints.FreezeRotationZ | 
                         RigidbodyConstraints.FreezePositionY;

        animator.ResetTrigger("Dash");
    }

    void Update()
    {
        if (!isDashing)
        {
            HandleMovementInput();
            HandleAnimations();
        }

        if (Input.GetButtonDown("Shoot"))
        {
            Debug.Log("Shoot button pressed!");
            if (gun != null) gun.Shoot();
            else Debug.LogError("Gun reference is missing in PlayerMovement!");
        }

        //  Dash Activation 
        if (Input.GetKeyDown(KeyCode.Space) && dashCooldownTimer <= 0 && !isDashing)
        {
            StartDash();
        }

        // ✅ Dash Cooldown Handling
        if (dashCooldownTimer > 0)
        {
            dashCooldownTimer -= Time.deltaTime;
        }
    }

    void FixedUpdate()
    {
        if (isDashing)
        {
            DashForward();
        }
        else
        {
            MovePlayer();
        }

        RotatePlayer();
    }

    void HandleMovementInput()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector3(-moveZ, 0, moveX).normalized;
    }

    void MovePlayer()
    {
        if (moveDirection.sqrMagnitude > 0.01f)
        {
            Vector3 targetPosition = rb.position + moveDirection * moveSpeed * Time.fixedDeltaTime;
            rb.MovePosition(targetPosition);
        }
    }

    void RotatePlayer()
    {
        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    void HandleAnimations()
    {
        bool isMoving = moveDirection.sqrMagnitude > 0.01f;
        animator.SetBool("isWalking", isMoving);
    }

    void StartDash()
    {
        Debug.Log("Dash Started!");
        animator.SetTrigger("isDashing");
        isDashing = true;

        Vector3 dashDirection = transform.forward;
        float intendedDashDistance = dashDistance;

        // Raycast to check for trees
        RaycastHit hit;
        bool obstacleDetected = Physics.Raycast(
            transform.position,
            dashDirection,
            out hit,
            dashDistance,
            LayerMask.GetMask("Tree")
        );

        // Adjust dash distance if a tree is detected
        if (obstacleDetected)
        {
            float buffer = 0.5f; // Prevents clipping into the tree
            intendedDashDistance = Mathf.Max(0, hit.distance - buffer);
        }

        dashTarget = transform.position + dashDirection * intendedDashDistance;
    }

    void DashForward()
    {
        // Move toward the dash target
        rb.MovePosition(Vector3.MoveTowards(
            transform.position,
            dashTarget,
            moveSpeed * 10f * Time.fixedDeltaTime
        ));

        // End the dash when the target is reached
        if (Vector3.Distance(transform.position, dashTarget) < 0.1f)
        {
            Debug.Log("Dash Ended!");
            isDashing = false;
            dashCooldownTimer = dashCooldown;
        }
    }

   void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Player Collided with: " + collision.gameObject.name);

        if (collision.gameObject.CompareTag("Tree"))
        {
            // Stop dashing if the player is currently dashing
            if (isDashing)
            {
                isDashing = false;
                dashCooldownTimer = dashCooldown;
            }

            // Push the player out of the trunk, but reduce the force during dash
            Vector3 collisionNormal = collision.contacts[0].normal;
            Vector3 pushbackPosition = transform.position + collisionNormal * 0.2f; // Reduced pushback distance
            rb.MovePosition(pushbackPosition);

            // Optional: Add a smaller force to push the player away
            rb.AddForce(collisionNormal * 2f, ForceMode.Impulse); // Reduced force
        }
    }
}
