using UnityEngine;

public class FlamethrowerEnemy : Enemy_Controller
{
   protected override void Attack()
    {
        animator.SetTrigger("Pickaxe_Attack");
        TheCastle.Apply_Damage(DamageToTake);
        Debug.Log("⛏️ Pickaxe Enemy attacked!");
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
