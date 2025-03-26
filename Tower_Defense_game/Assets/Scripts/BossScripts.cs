// using UnityEngine;

// public class BossScripts : MonoBehaviour
// {
//     [Header("References")]
//     public Transform[] firePoints; // Assign manually in Inspector
//     public GameObject slamEffect;
//     public Animator animator;
//     public Transform target;

//     [Header("Ranged Attack Settings")]
//     public float shootCooldown = 1f;
//     public float rangedStopDistance = 20f;

//     [Header("Ultimate Settings")]
//     public float ultimateCooldown = 10f;

//     private bool isUlting = false;
//     private float shootTimer = 0f;
//     private float ultimateTimer = 0f;
//     private Rigidbody rb;

//     void Start()
//     {
//         rb = GetComponent<Rigidbody>();

//         // Auto-assign Animator if not manually set
//         if (animator == null)
//         {
//             animator = GetComponent<Animator>();
//             if (animator == null)
//                 animator = GetComponentInChildren<Animator>();
//         }

//         shootTimer = shootCooldown;
//         ultimateTimer = ultimateCooldown;
//     }

//     void Update()
//     {
//         if (!LevelManager.instance.levelActive || target == null) return;

//         float distance = Vector3.Distance(transform.position, target.position);

//         if (isUlting) return;

//         if (distance > rangedStopDistance)
//         {
//             MoveTowardsTarget();
//             SetWalkingAnimation(true);
//         }
//         else
//         {
//             rb.linearVelocity = Vector3.zero;
//             SetWalkingAnimation(false);

//             // Face target
//             Vector3 direction = target.position - transform.position;
//             direction.y = 0;
//             transform.rotation = Quaternion.LookRotation(direction.normalized);

//             // Regular attack
//             shootTimer -= Time.deltaTime;
//             if (shootTimer <= 0f)
//             {
//                 FireAllCannons();
//                 shootTimer = shootCooldown;
//             }

//             // Trigger ultimate if ready
//             ultimateTimer -= Time.deltaTime;
//             if (ultimateTimer <= 0f)
//             {
//                 TriggerUltimate();
//                 ultimateTimer = ultimateCooldown;
//             }
//         }
//     }

//     void MoveTowardsTarget()
//     {
//         Vector3 dir = (target.position - transform.position).normalized;
//         rb.linearVelocity = new Vector3(dir.x, rb.linearVelocity.y, dir.z);
//     }

//     void SetWalkingAnimation(bool isWalking)
//     {
//         if (animator != null)
//             animator.SetBool("IsWalkingBoss", isWalking);
//     }

//     void FireAllCannons()
//     {
//         foreach (Transform point in firePoints)
//         {
//             GameObject bullet = BulletPool.Instance.GetBullet();
//             if (bullet == null) continue;

//             Vector3 dir = (target.position - point.position).normalized;

//             bullet.transform.position = point.position;
//             bullet.transform.rotation = Quaternion.LookRotation(dir);

//             Enemy_Bullet bulletScript = bullet.GetComponent<Enemy_Bullet>();
//             if (bulletScript != null)
//                 bulletScript.Initialize(dir);
//         }
//     }

//     void TriggerUltimate()
//     {
//         isUlting = true;
//         if (animator != null)
//             animator.SetTrigger("Slam");

//         Invoke(nameof(ExecuteUltimate), 0.7f); // Match slam animation timing
//     }

//     public void ExecuteUltimate()
//     {
//         if (slamEffect != null)
//             Instantiate(slamEffect, transform.position, Quaternion.identity);

//         foreach (Transform point in firePoints)
//         {
//             GameObject bullet = BulletPool.Instance.GetBullet();
//             if (bullet == null) continue;

//             Vector3 dir = (target.position - point.position).normalized;

//             bullet.transform.position = point.position;
//             bullet.transform.rotation = Quaternion.LookRotation(dir);

//             Enemy_Bullet bulletScript = bullet.GetComponent<Enemy_Bullet>();
//             if (bulletScript != null)
//                 bulletScript.Initialize(dir * 2f); // More powerful ult shot
//         }

//         isUlting = false;
//     }
// }
