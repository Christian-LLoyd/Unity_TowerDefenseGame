using UnityEngine;

public class Enemy_Controller : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Transform target;
    public int health = 150;
    public float stopDistance = 2f; // Distance at which enemy stops moving
    public float timeBetweenAttacks, perAttack;
    public float attackCounter;
    public float DamageToTake = 10f; // Adjust this damage value as needed

    private Rigidbody rb;
    private Target_Castle TheCastle;
    //testttt

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


        attackCounter = timeBetweenAttacks = 5f;
        TheCastle = FindObjectOfType<Target_Castle>(); 

        if (TheCastle == null)
        {
            Debug.LogError("⚠️ TheCastle is NULL! Make sure the Target_Castle script is on the correct GameObject.");
        }


    }

    void Update()
    {
        MoveTowardsTarget();
        attackCounter -= Time.deltaTime;

        if (attackCounter <= 0 && TheCastle != null && Vector3.Distance(transform.position, target.position) <= stopDistance)
        {
            attackCounter = timeBetweenAttacks; // ✅ Reset the attack cooldown
            TheCastle.Apply_Damage(DamageToTake);
            Debug.Log("⚔️ Enemy attacked the castle! Distance: " + Vector3.Distance(transform.position, target.position));
        }

    }


    void MoveTowardsTarget()
    {
        if (target == null) return;

        float distance = Vector3.Distance(transform.position, target.position); 

        if (distance > stopDistance)
        {
            
            Vector3 direction = (target.position - transform.position).normalized;
            direction.y = 0; 

            rb.linearVelocity = direction * moveSpeed;

          
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
        if (this == null) return; 

        health -= damage;
        Debug.Log(gameObject.name + " took damage! Remaining HP: " + health);

        if (health <= 0)
        {
            Debug.Log(gameObject.name + " destroyed!");
            Destroy(gameObject);
        }
    }

    //debug for enemy close to the player
 
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
