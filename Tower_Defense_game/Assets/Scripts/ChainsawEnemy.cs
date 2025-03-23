using UnityEngine;

public class ChainsawEnemy : Enemy_Controller
{
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
        animator.SetTrigger("Chainsaw_Attack");
        TheCastle.Apply_Damage(DamageToTake);
        Debug.Log("Chainsaw Enemy attacked!");
    }

    protected override void SetWalkingAnimation(bool isWalking)
    {
        animator.SetBool("IsWalkingChainsaw", isWalking);
    }

    protected override void Die()
    {
        animator.SetTrigger("Chainsaw_Death");
    }
}
