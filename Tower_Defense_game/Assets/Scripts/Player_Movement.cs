using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float moveSpeed = 10f;
    public float rotationSpeed = 10f;

    private Rigidbody rb;
    public Player_Bullet gun;
    private Vector3 moveDirection;
    private Vector3 shootDirection;
    private bool recentlyShot; // Tracks recent shooting
    private Vector3 shootDirection;
    private bool recentlyShot; // Tracks recent shooting
    private Animator animator;

    // Shooting Cooldown (helps prioritize shooting rotation temporarily)
    private float shootCooldownTimer = 0f;
    public float shootCooldownDuration = 0.3f;

    // Shooting Cooldown (helps prioritize shooting rotation temporarily)
    private float shootCooldownTimer = 0f;
    public float shootCooldownDuration = 0.3f;

    // Dash Variables
    public float dashDistance = 5f;
<<<<<<< Updated upstream
    public float dashDistance = 5f; // ✅ Fixed distance dash
=======
    public float dashDistance = 5f;
>>>>>>> Stashed changes
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
        rb.constraints = RigidbodyConstraints.FreezeRotationX |
                         RigidbodyConstraints.FreezeRotationZ |
                         RigidbodyConstraints.FreezePositionY;

        animator.ResetTrigger("Dash");

        if (gun != null)
        {
            gun.OnShootDirection += RotatePlayerToShootDirection;
        }

        if (gun != null)
        {
            gun.OnShootDirection += RotatePlayerToShootDirection;
        }
    }

    void Update()
    {
        if (!LevelManager.instance.levelActive) return; // Stop movement if the game has ended

        if (!LevelManager.instance.levelActive) return; // Stop movement if the game has ended

        if (!isDashing)
        {
            HandleMovementInput();
            HandleAnimations();
        }

        if (Input.GetButtonDown("Shoot"))
        {
            if (gun != null)
            {
                gun.Shoot();
                animator.SetTrigger("Shoot");

                recentlyShot = true;
                shootCooldownTimer = shootCooldownDuration; // Shooting cooldown activated
            }
            else
            {
                Debug.LogError("Gun reference is missing in PlayerMovement!");
            }
        }

            if (gun != null)
            {
                gun.Shoot();
                animator.SetTrigger("Shoot");

                recentlyShot = true;
                shootCooldownTimer = shootCooldownDuration; // Shooting cooldown activated
            }
            else
            {
                Debug.LogError("Gun reference is missing in PlayerMovement!");
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && dashCooldownTimer <= 0 && !isDashing)
        {
            StartDash();
        }

        if (dashCooldownTimer > 0)
        {
            dashCooldownTimer -= Time.deltaTime;
        }

        // Handle Shooting Cooldown
        if (recentlyShot && shootCooldownTimer > 0)
        {
            shootCooldownTimer -= Time.deltaTime;
        }
        else
        {
            recentlyShot = false;
        }
    }

    // Rotate toward shooting direction when shooting occurs
    void RotatePlayerToShootDirection(Vector3 direction)
    {
        shootDirection = direction;
        transform.rotation = Quaternion.LookRotation(shootDirection); 

        // Handle Shooting Cooldown
        if (recentlyShot && shootCooldownTimer > 0)
        {
            shootCooldownTimer -= Time.deltaTime;
        }
        else
        {
            recentlyShot = false;
        }
    }

    // Rotate toward shooting direction when shooting occurs
    void RotatePlayerToShootDirection(Vector3 direction)
    {
        shootDirection = direction;
        transform.rotation = Quaternion.LookRotation(shootDirection); 
    }

    void FixedUpdate()
    {
        if (!LevelManager.instance.levelActive) return; // Stop movement if the game has ended

        if (!LevelManager.instance.levelActive) return; // Stop movement if the game has ended

        if (isDashing)
        {
            DashForward();
        }
        else
        {
            MovePlayer();
        }

        // Priority: Shooting rotation first, otherwise movement rotation
        if (recentlyShot)
        {
            transform.rotation = Quaternion.LookRotation(shootDirection);
        }
        else
        {
            RotatePlayer();
        }
        // Priority: Shooting rotation first, otherwise movement rotation
        if (recentlyShot)
        {
            transform.rotation = Quaternion.LookRotation(shootDirection);
        }
        else
        {
            RotatePlayer();
        }
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
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);

            // Instantly rotate to face movement direction with shortest path logic
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 720f * Time.deltaTime);
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);

            // Instantly rotate to face movement direction with shortest path logic
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 720f * Time.deltaTime);
        }
    }

    void HandleAnimations()
    {
        bool isMoving = moveDirection.sqrMagnitude > 0.01f;
        animator.SetBool("isWalking", isMoving);

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Kapre_Shoot"))
        {
            animator.SetBool("isWalking", false);
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Kapre_Shoot"))
        {
            animator.SetBool("isWalking", false);
        }
    }

    void StartDash()
<<<<<<< Updated upstream
    {   Debug.Log("the start() called lmaio");
=======
    {
>>>>>>> Stashed changes
        animator.SetTrigger("isDashing");
        // Debug.Log(" Dash Activated!");
        isDashing = true;
<<<<<<< Updated upstream
        dashTarget = transform.position + transform.forward * dashDistance; // Moves forward a fixed distance
=======

        Vector3 dashDirection = transform.forward;
        float intendedDashDistance = dashDistance;

        RaycastHit hit;
        bool obstacleDetected = Physics.Raycast(
            transform.position,
            dashDirection,
            out hit,
            dashDistance,
            LayerMask.GetMask("Tree")
        );

        if (obstacleDetected)
        {
            float buffer = 0.5f;
            float buffer = 0.5f;
            intendedDashDistance = Mathf.Max(0, hit.distance - buffer);
        }

        dashTarget = transform.position + dashDirection * intendedDashDistance;
>>>>>>> Stashed changes
    }

    void DashForward()
    {
<<<<<<< Updated upstream
        // ✅ Move towards dash target
        rb.MovePosition(Vector3.MoveTowards(transform.position, dashTarget, moveSpeed * 10f * Time.fixedDeltaTime));

        
        if (Vector3.Distance(transform.position, dashTarget) < 0.1f)
        {
            Debug.Log("🛑 Dash Ended!");
=======
        rb.MovePosition(Vector3.MoveTowards(
            transform.position,
            dashTarget,
            moveSpeed * 10f * Time.fixedDeltaTime
        ));

        if (Vector3.Distance(transform.position, dashTarget) < 0.1f)
        {
>>>>>>> Stashed changes
            isDashing = false;
            dashCooldownTimer = dashCooldown;
        }
    }

    void OnCollisionEnter(Collision collision)
    void OnCollisionEnter(Collision collision)
    {
<<<<<<< Updated upstream
        Debug.Log("🔥 Player Collided with: " + collision.gameObject.name);
=======
        if (collision.gameObject.CompareTag("Tree"))
        {
            if (isDashing)
            {
                isDashing = false;
                dashCooldownTimer = dashCooldown;
            }

            Vector3 collisionNormal = collision.contacts[0].normal;
            Vector3 pushbackPosition = transform.position + collisionNormal * 0.2f;
            Vector3 pushbackPosition = transform.position + collisionNormal * 0.2f;
            rb.MovePosition(pushbackPosition);

            rb.AddForce(collisionNormal * 2f, ForceMode.Impulse);
            rb.AddForce(collisionNormal * 2f, ForceMode.Impulse);
        }
>>>>>>> Stashed changes
    }
}
