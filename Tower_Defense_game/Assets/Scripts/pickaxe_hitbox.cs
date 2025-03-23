// using UnityEngine;

// public class PickaxeHitbox : MonoBehaviour
// {
//     [SerializeField] private PickaxeEnemy pickaxeEnemy; // Reference to PickaxeEnemy

//     private void OnTriggerEnter(Collider other)
//     {
//         if (other.CompareTag("Player") || other.CompareTag("Tree"))
//         {
//             Debug.Log("💥 Hit something!");

//             if (pickaxeEnemy != null)
//             {
//                 pickaxeEnemy.TriggerAttack();  // Uses existing Attack logic
//             }
//         }
//     }

//     private void OnEnable()
//     {
//         Debug.Log("✅ Pickaxe Hitbox Activated");
//     }

//     private void OnDisable()
//     {
//         Debug.Log("❌ Pickaxe Hitbox Deactivated");
//     }
// }
