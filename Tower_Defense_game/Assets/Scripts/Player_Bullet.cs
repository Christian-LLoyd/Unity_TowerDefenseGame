using UnityEngine;

public class Player_Bullet : MonoBehaviour
{
    public Transform spawn;
    public Camera mainCamera;
    public GameObject bulletPrefab;
    public float bulletSpeed = 50f;
    public int bulletDamage = 150;

    public Transform gunTip; //  Assign the arm transform (arm stays static)
    public void Shoot()
    {
        if (gunTip == null || mainCamera == null || bulletPrefab == null)
        {
            Debug.LogError("Missing references! Make sure gunTip is assigned.");
            return;
        }

        // Raycast to find where the cursor is pointing
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Vector3 targetPoint;

        if (Physics.Raycast(ray, out hit))
        {
            targetPoint = hit.point; // Cursor hit point
        }
        else
        {
            targetPoint = ray.origin + ray.direction * 50f; // Fallback if no hit
        }

    
        targetPoint.y = gunTip.position.y; // Keep it on the same height as the arm

        Vector3 direction = (targetPoint - gunTip.position).normalized;

        //Visualize aiming
        Debug.DrawRay(gunTip.position, direction * 10, Color.red, 2f);
        Debug.DrawLine(gunTip.position, targetPoint, Color.green, 2f);
        Debug.Log($"🎯 Target Position: {targetPoint} | 🏹 Bullet Spawn: {gunTip.position}");

        // Fire bullet at gunTip
        GameObject bullet = Instantiate(bulletPrefab, gunTip.position, Quaternion.LookRotation(direction));

        // Configure Rigidbody
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = direction * bulletSpeed; // ✅ Bullet moves correctly
            rb.useGravity = false;
            rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        }
        else
        {
            Debug.LogError("Bullet is missing a Rigidbody!");
        }

        // ✅ Destroy bullet after 3 seconds
        Destroy(bullet, 3f);
    }


    // Fixed Bullet Collisiom
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("🔥 Bullet hit: " + other.gameObject.name + " | Tag: " + other.tag);

        if (other.CompareTag("Enemy"))
        {
            Debug.Log("✅ Enemy Hit! Applying Damage...");
            
            // Damage enemy
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
