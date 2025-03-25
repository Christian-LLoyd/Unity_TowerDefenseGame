using UnityEngine;

public class CarEnemy : Enemy_Movements
{
    public void TriggerAttack()
    {
        if (TheCastle != null)
        {
            TheCastle.Apply_Damage(DamageToTake); // Damage the target
            Debug.Log("park took " + DamageToTake + " damage!");
        }
    }

    protected override void Attack()
    {

        if (TheCastle != null)
        {
            TheCastle.Apply_Damage(DamageToTake);
            Debug.Log("car Enemy attacked!");
        }
    }

    protected override void SetWalkingAnimation(bool isWalking)
    {
        animator.SetBool("Red_car_Move", isWalking);
    }

    protected override void Die()
    {
        Destroy(gameObject, 1.5f); // Delay destruction for animation
    }
}
