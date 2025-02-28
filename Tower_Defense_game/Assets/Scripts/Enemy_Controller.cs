using UnityEngine;

public class Enemy_Controller : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Transform target;
    public int health = 150;
    public float stopDistance = 2f; // Distance at which enemy stops moving

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (!rb)
        {
            Debug.LogError("Rigidbody missing on " + gameObject.name);
        }

        // ✅ Ensure proper Rigidbody settings
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
        MoveTowardsTarget();
    }

    void MoveTowardsTarget()
    {
        if (target == null) return;

        float distance = Vector3.Distance(transform.position, target.position);

        if (distance > stopDistance)
        {
            // ✅ Move enemy toward player
            Vector3 direction = (target.position - transform.position).normalized;
            direction.y = 0; // Prevents enemy from floating

            rb.linearVelocity = direction * moveSpeed;

            // ✅ Rotate enemy to face player
            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 5f * Time.deltaTime);
            }
        }
        else
        {
            rb.linearVelocity = Vector3.zero; // ✅ Stop enemy when close to player
        }
    }

    public void DamageEnemy(int damage)
    {
        if (this == null) return; // ✅ Prevents error if enemy is already destroyed

        health -= damage;
        Debug.Log(gameObject.name + " took damage! Remaining HP: " + health);

        if (health <= 0)
        {
            Debug.Log(gameObject.name + " destroyed!");
            Destroy(gameObject);
        }
    }


    // ✅ Debug: Triggered when enemy enters the player's collider
   private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("⚠️ Enemy is close to the player!");
        }

        if (other.CompareTag("Bullet"))
        {
            Debug.Log("🔥 Enemy was hit by a bullet: " + other.gameObject.name);
            DamageEnemy(50); // Example damage
            Destroy(other.gameObject); // Remove bullet on hit
        }
    }


    // ✅ Debug: Triggered when enemy exits the player's collider
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Enemy is no longer close to the player.");
        }
    }
}
