using UnityEngine;

public class FlamethrowerEnemy : Enemy_Controller
{
    [Header("Ranged Attack Settings")]
    public GameObject enemyBulletPrefab;
    public Transform firePoint;
    public float shootCooldown = 0.3f; // Independent fire rate
    public float rangedStopDistance = 10f; // Ideal shooting distance

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

        if (enemyBulletPrefab != null && firePoint != null)
        {
            Vector3 playerDirection = (GetPlayerPosition() - firePoint.position);
            playerDirection.y = 0; // Prevent aiming up/down
            playerDirection.Normalize();

            Quaternion bulletRotation = Quaternion.LookRotation(playerDirection);

            Instantiate(enemyBulletPrefab, firePoint.position, bulletRotation);

            Debug.Log("🔥 Flamethrower Enemy shot a projectile!");
        }
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
