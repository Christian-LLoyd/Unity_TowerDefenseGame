using UnityEngine;

public class Enemy_Bullet : MonoBehaviour
{
    public float speed = 4f;
    public int damage = 10; // 💥 New: damage value

    private Vector3 direction;
    private Transform playerTransform;

    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            playerTransform = playerObj.transform;
            Vector3 flatTarget = new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z);
            direction = (flatTarget - transform.position).normalized;
        }
        else
        {
            Debug.LogWarning("Player not found by Enemy_Bullet!");
            direction = Vector3.forward;
        }
    }

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement player = other.GetComponent<PlayerMovement>();
            if (player != null)
            {
                player.TakeDamage(damage); 
            }

            Destroy(gameObject);
        }
        else if (!other.CompareTag("Enemy") && !other.CompareTag("EnemyBullet"))
        {
            Destroy(gameObject);
        }
    }
}
