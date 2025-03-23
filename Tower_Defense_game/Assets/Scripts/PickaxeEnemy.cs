using UnityEngine;

public class PickaxeEnemy : Enemy_Controller
{
     // Public method to trigger Attack() safely
    public void TriggerAttack()
    {
        if (TheCastle != null)
        {
            TheCastle.Apply_Damage(DamageToTake); // Damage the tree/castle
            Debug.Log("🌳 The Castle took " + DamageToTake + " damage!");
        }
    }

   protected override void Attack()
    {
        if (target == null) return;

        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        if (distanceToTarget > stopDistance)
        {
            Debug.Log("❌ Player moved out of range before attack finished.");
            return; // Remove the ResetTrigger to avoid canceling the animation
        }

        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Attack_Pickaxe"))
        {
            animator.SetTrigger("Attack_Pickaxe");
            Debug.Log("⛏️ Pickaxe Enemy attacked!");
        }
    }


    protected override void SetWalkingAnimation(bool isWalking)
    {
        animator.SetBool("IsWalking", isWalking);
    }

    protected override void Die()
    {
        animator.SetTrigger("Death_Pickaxe");
    }
}
