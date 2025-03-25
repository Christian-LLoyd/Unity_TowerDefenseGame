using UnityEngine;

public abstract class Enemy_Controller : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Transform target;
    public int health = 150;
    public float stopDistance = 2f;
    public float timeBetweenAttacks = 5f;
    public float attackCounter;
    public float DamageToTake = 10f;
    public float damageMultiplier = 1f;
    protected Rigidbody rb;
    protected Target_Castle TheCastle;
    protected Animator animator;

    void Start()
    {
        LevelManager.instance.activeEnemies.Add(this);

        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        if (!rb)
        {
            Debug.LogError("Rigidbody missing on " + gameObject.name);
        }

        rb.freezeRotation = true;
        rb.isKinematic = false;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.constraints = RigidbodyConstraints.FreezeRotationX |
                         RigidbodyConstraints.FreezeRotationZ |
                         RigidbodyConstraints.FreezePositionY;

        attackCounter = timeBetweenAttacks;
        TheCastle = Object.FindFirstObjectByType<Target_Castle>();

        if (TheCastle == null)
        {
            Debug.LogError("⚠️ TheCastle is NULL! Make sure the Target_Castle script is on the correct GameObject.");
        }
    }
    //test code for attack range
     public bool hasEnteredAttackRange = false;

    void Update()
    {
        if (!LevelManager.instance.levelActive)
        {
            StopMovementAndAnimations();
            return;  // Prevents further movement or attack logic
        }

        MoveTowardsTarget();
        HandleAttack();
    }

    void MoveTowardsTarget()
    {
        if (target == null) return;

        float distance = Vector3.Distance(transform.position, target.position);

        if (distance > stopDistance)
        {
            hasEnteredAttackRange = false; 
            Vector3 direction = (target.position - transform.position).normalized;
            direction.y = 0;

            rb.linearVelocity = direction * moveSpeed;  // Updated for consistency in Rigidbody
            SetWalkingAnimation(true);

            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 5f * Time.deltaTime);
            }
        }
        else
        {
            if (!hasEnteredAttackRange)
            {
                hasEnteredAttackRange = true;
                attackCounter = 0; // Force immediate attack
            }
            rb.linearVelocity = Vector3.zero;
            SetWalkingAnimation(false);
        }
    }

    void HandleAttack()
    {
        if (!LevelManager.instance.levelActive) return;

        attackCounter -= Time.deltaTime;

        if (attackCounter <= 0 && TheCastle != null && Vector3.Distance(transform.position, target.position) <= stopDistance)
        {
            Attack(); // Trigger Attack() here
            float modifiedDamage = DamageToTake * damageMultiplier;
            TheCastle.Apply_Damage(modifiedDamage);
            attackCounter = timeBetweenAttacks; // Reset cooldown AFTER Attack()
            
        }
    }

    public void DamageEnemy(int damage)
    {
        if (this == null) return;

        health -= damage;
        Debug.Log(gameObject.name + " took damage! Remaining HP: " + health);

        if (health <= 0)
        {
            health = 0;

            LevelManager.instance.activeEnemies.Remove(this);

            Die();
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (LevelManager.instance != null)
        {
            LevelManager.instance.activeEnemies.Remove(this);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("⚠️ Enemy is close to the player!");
        }

        if (other.CompareTag("Bullet"))
        {
            Debug.Log("🔥 Enemy was hit by a bullet: " + other.gameObject.name);
            DamageEnemy(50);
            Destroy(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Enemy is no longer close to the player.");
        }
    }

    // Stops all animations and movement immediately when the game ends
    private void StopMovementAndAnimations()
    {
        rb.linearVelocity = Vector3.zero; // Stops Rigidbody movement
        if (animator != null)
        {
            animator.speed = 0; // Pauses animation
        }
    }

    protected abstract void Attack();
    protected abstract void SetWalkingAnimation(bool isWalking);
    protected abstract void Die();
}
