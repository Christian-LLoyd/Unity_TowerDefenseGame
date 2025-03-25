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
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        if (!rb)
            Debug.LogError("❌ Rigidbody missing on " + gameObject.name);

        rb.freezeRotation = true;
        rb.isKinematic = false;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.constraints = RigidbodyConstraints.FreezeRotationX |
                         RigidbodyConstraints.FreezeRotationZ |
                         RigidbodyConstraints.FreezePositionY;

        attackCounter = timeBetweenAttacks;
        TheCastle = FindObjectOfType<Target_Castle>();

        if (TheCastle == null)
            Debug.LogError("⚠ TheCastle is NULL! Make sure it's assigned properly.");
    }

    void Update()
    {
        MoveTowardsTarget();
        HandleAttack();
    }

    void MoveTowardsTarget()
    {
        if (target == null) return;

        float distance = Vector3.Distance(transform.position, target.position);

        if (distance > stopDistance)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            direction.y = 0;

            rb.velocity = direction * moveSpeed; // Rigidbody movement
            SetWalkingAnimation(true);

            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 5f * Time.deltaTime);
            }
        }
        else
        {
            rb.velocity = Vector3.zero;
            SetWalkingAnimation(false);
        }
    }

    void HandleAttack()
    {
        attackCounter -= Time.deltaTime;

        if (attackCounter <= 0 && TheCastle != null && Vector3.Distance(transform.position, target.position) <= stopDistance)
        {
            Attack(); // Trigger Attack() method
            float modifiedDamage = DamageToTake * damageMultiplier;
            TheCastle.Apply_Damage(modifiedDamage);
            attackCounter = timeBetweenAttacks; // Reset cooldown
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
            Die();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            Debug.Log("⚠ Enemy is near the player!");

        if (other.CompareTag("Bullet"))
        {
            Debug.Log("🔥 Enemy hit by a bullet: " + other.gameObject.name);
            DamageEnemy(50);
            Destroy(other.gameObject);
        }
    }

    protected abstract void Attack();
    protected abstract void SetWalkingAnimation(bool isWalking);
    protected abstract void Die();
}
