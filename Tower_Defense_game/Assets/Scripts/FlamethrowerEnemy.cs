using UnityEngine;

public class FlamethrowerEnemy : Enemy_Controller
{
    [Header("Ranged Attack Settings")]
    public GameObject enemyBulletPrefab;
    public Transform firePoint;
    public float shootCooldown = 0.5f;
    public float rangedStopDistance = 15f;

    private float shootTimer = 0f;

    void Update()
    {
        if (!LevelManager.instance.levelActive || target == null) return;

        float distance = Vector3.Distance(transform.position, target.position);

        if (distance > rangedStopDistance)
        {
            MoveTowardsTarget();
            SetWalkingAnimation(true);
        }
        else
        {
            rb.linearVelocity = Vector3.zero;
            SetWalkingAnimation(false);

            Vector3 direction = (target.position - transform.position);
            direction.y = 0;
            transform.rotation = Quaternion.LookRotation(direction.normalized);

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
        if (firePoint == null || BulletPool.Instance == null)
        {
            Debug.LogError("❌ Missing firePoint or BulletPool instance.");
            return;
        }

        Vector3 direction = GetPlayerPosition() - firePoint.position;
        direction.y = 0;
        direction.Normalize();

        GameObject bullet = BulletPool.Instance.GetBullet();
        if (bullet == null)
        {
            Debug.LogWarning("⚠️ No bullet returned from pool.");
            return;
        }

        bullet.transform.position = firePoint.position;
        bullet.transform.rotation = Quaternion.LookRotation(direction);

        Enemy_Bullet bulletScript = bullet.GetComponent<Enemy_Bullet>();
        if (bulletScript != null)
        {
            bulletScript.Initialize(direction);
        }

        Debug.Log("🔥 Pooled bullet fired!");
    }

    private Vector3 GetPlayerPosition()
    {
        return target != null ? target.position : firePoint.position + transform.forward * 5f;
    }

    protected override void Attack() { }

    protected override void SetWalkingAnimation(bool isWalking)
    {
        animator.SetBool("IsWalkingFlamethrower", isWalking);
    }

    protected override void Die()
    {
        animator.SetTrigger("Flamethrower_Death");
    }
}
