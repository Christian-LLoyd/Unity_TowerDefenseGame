using UnityEngine;

public class Enemy_Bullet : MonoBehaviour
{
    public float speed = 4f;
    public int damage = 10;
    public float lifetime = 5f;
    
    private Vector3 direction;
    private float currentLifetime;

    // Updated to accept direction directly
    public void Initialize(Vector3 fireDirection)
    {
        direction = fireDirection.normalized;
        currentLifetime = 0f;
    }

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
        
        currentLifetime += Time.deltaTime;
        if (currentLifetime >= lifetime)
        {
            ReturnToPool();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement player = other.GetComponent<PlayerMovement>();
            if (player != null) player.TakeDamage(damage);
            ReturnToPool();
        }
        else if (!other.CompareTag("Enemy") && !other.CompareTag("EnemyBullet"))
        {
            ReturnToPool();
        }
    }

    private void ReturnToPool()
    {
        if (BulletPool.Instance != null)
        {
            BulletPool.Instance.ReturnBullet(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnEnable()
    {
        currentLifetime = 0f; // to reuse the bullet pool
        Debug.Log("✅ Bullet enabled from pool!");
    }

}