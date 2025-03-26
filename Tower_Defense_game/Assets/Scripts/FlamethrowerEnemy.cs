using UnityEngine;

public class FlamethrowerEnemy : Enemy_Controller
{
    [Header("Ranged Attack Settings")]
    public GameObject enemyBulletPrefab;
    public Transform firePoint;
    public float shootCooldown = 0.5f; //  fire rate
    public float rangedStopDistance = 15f; //  shooting distance

    private float shootTimer = 0f;

    void Update()
    {
        if (!LevelManager.instance.levelActive || target == null) return;

        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        if (distanceToTarget > rangedStopDistance)
        {
            // Too far — move toward the target
            MoveTowardsTarget();
            SetWalkingAnimation(true);
        }
        else
        {
            // In firing range — stop and shoot
            rb.linearVelocity = Vector3.zero;
            SetWalkingAnimation(false);

            transform.rotation = Quaternion.LookRotation((target.position - transform.position).normalized);

            shootTimer -= Time.deltaTime;
            if (shootTimer <= 0f)
            {
                ShootProjectile();
                shootTimer = shootCooldown;
            }
        }
    }

   private void ShootProjectile()
    {
        Debug.Log("🔥 Attempting to shoot...");

        if (firePoint == null)
        {
            Debug.LogError("❌ FirePoint is not assigned.");
            return;
        }

        if (BulletPool.Instance == null)
        {
            Debug.LogError("❌ BulletPool instance is missing in scene.");
            return;
        }

        Vector3 playerDirection = (GetPlayerPosition() - firePoint.position);
        playerDirection.y = 0; // Prevent aiming up/down
        playerDirection.Normalize();

        Quaternion bulletRotation = Quaternion.LookRotation(playerDirection);

        GameObject bullet = BulletPool.Instance.GetBullet();
        bullet.transform.position = firePoint.position;
        bullet.transform.rotation = bulletRotation;

        Enemy_Bullet bulletScript = bullet.GetComponent<Enemy_Bullet>();
        if (bulletScript != null)
        {
            bulletScript.Initialize(playerDirection); // pass direction directly
        }

        Debug.Log("🔥 Flamethrower Enemy shot a pooled projectile!");
    }


    private Vector3 GetPlayerPosition()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        return player != null ? player.transform.position : firePoint.position + transform.forward * 5f;
    }

    protected override void Attack()
    {
        // Not needed for ranged logic
    }

    protected override void SetWalkingAnimation(bool isWalking)
    {
        animator.SetBool("IsWalkingFlamethrower", isWalking);
    }

    protected override void Die()
    {
        animator.SetTrigger("Flamethrower_Death");
    }
}
