using UnityEngine;

public class Player_Bullet : MonoBehaviour
{
    public Transform spawn;
    public Camera mainCamera;
    public GameObject bulletPrefab;
    public float bulletSpeed = 50f;
    public int bulletDamage = 150;

    public Transform gunTip;

    // Delegate to notify PlayerMovement of shoot direction
    public System.Action<Vector3> OnShootDirection;

    public void Shoot()
    {
        // Prevent shooting if the game is over or paused
        if (!LevelManager.instance.levelActive || Time.timeScale == 0f)
        {
            Debug.Log("❌ Shooting disabled (Game paused or ended)");
            return;
        }

        if (gunTip == null || mainCamera == null || bulletPrefab == null)
        {
            Debug.LogError("Missing references! Make sure gunTip is assigned.");
            return;
        }

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Vector3 targetPoint;

        if (Physics.Raycast(ray, out hit))
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = ray.origin + ray.direction * 50f;
        }

        targetPoint.y = gunTip.position.y;

        Vector3 direction = (targetPoint - gunTip.position).normalized;

        // Notify PlayerMovement to rotate instantly toward the shooting direction
        OnShootDirection?.Invoke(direction);

        GameObject bullet = Instantiate(bulletPrefab, gunTip.position, Quaternion.LookRotation(direction));

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = direction * bulletSpeed;
            rb.useGravity = false;
            rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        }

        Destroy(bullet, 3f); // Bullet gets destroyed after 3 seconds
    }

    void Update()
    {
        // Destroy bullets only if they are already active when the game ends
        if (!LevelManager.instance.levelActive)
        {
            if (gameObject.activeInHierarchy)
            {
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("✅ Enemy Hit! Applying Damage...");

            Enemy_Controller enemy = other.GetComponent<Enemy_Controller>();
            if (enemy != null)
            {
                enemy.DamageEnemy(bulletDamage);
            }
        }

        Destroy(gameObject); // Destroy bullet on collision
    }
}
