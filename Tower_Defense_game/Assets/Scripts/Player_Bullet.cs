using UnityEngine;

public class Player_Bullet : MonoBehaviour
{
    public Transform spawn;
    public Camera mainCamera;
    public GameObject bulletPrefab;
    public float bulletSpeed = 50f;
    public int bulletDamage = 150;

    public void Shoot()
    {
        if (spawn == null || mainCamera == null || bulletPrefab == null)
        {
            Debug.LogError("Spawn, Camera, or Bullet Prefab is not assigned!", this);
            return;
        }

        // Cast a ray from the mouse position
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

        targetPoint.y = spawn.position.y; // Keeps bullet horizontal
        Vector3 direction = (targetPoint - spawn.position).normalized;

        // Instantiate bullet
        GameObject bullet = Instantiate(bulletPrefab, spawn.position, Quaternion.LookRotation(direction));

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = direction * bulletSpeed; // ✅ Correctly set linear velocity
            rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
            rb.useGravity = false;
            rb.interpolation = RigidbodyInterpolation.Interpolate;
        }
        else
        {
            Debug.LogError("Bullet is missing a Rigidbody!");
        }

        // Adjust Particle System Direction
        ParticleSystem ps = bullet.GetComponentInChildren<ParticleSystem>();
        if (ps != null)
        {
            ps.transform.rotation = Quaternion.LookRotation(direction);
        }

        Destroy(bullet, 3f); // Bullet despawns after 3 seconds
    }

    // ✅ Fixed Bullet Collision - No More Bullet Flying Away
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("🔥 Bullet hit: " + other.gameObject.name + " | Tag: " + other.tag);

        if (other.CompareTag("Enemy"))
        {
            Debug.Log("✅ Enemy Hit! Applying Damage...");
            
            // ✅ Damage enemy
            Enemy_Controller enemy = other.GetComponent<Enemy_Controller>();
            if (enemy != null)
            {
                enemy.DamageEnemy(bulletDamage);
            }
        }

        // ✅ Destroy bullet immediately on ANY hit (enemy, tree, ground, etc.)
        Destroy(gameObject);
    }

}
