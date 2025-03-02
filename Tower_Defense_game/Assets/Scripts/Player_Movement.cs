using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float rotationSpeed = 10f;

    private Rigidbody rb;
    public Player_Bullet gun;
    private Vector3 moveDirection;
    private Animator animator;

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
    }

    void Update()
{
    HandleMovementInput();
    HandleAnimations();

    if (Input.GetButtonDown("Shoot"))
    {
        Debug.Log("Shoot button pressed!"); // ✅ Debugging: Check if input is detected
        if (gun != null)
        {
            gun.Shoot();
        }
        else
        {
            Debug.LogError("Gun reference is missing in PlayerMovement!");
        }
    }
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

        moveDirection = new Vector3(-moveZ, 0, moveX).normalized;
    }

  void MovePlayer()
    {
        if (moveDirection.sqrMagnitude > 0.01f)
        {
            Vector3 targetVelocity = moveDirection * moveSpeed;
            rb.linearVelocity = new Vector3(targetVelocity.x, rb.linearVelocity.y, targetVelocity.z);
        }
        else
        {
            rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 0);
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
}
