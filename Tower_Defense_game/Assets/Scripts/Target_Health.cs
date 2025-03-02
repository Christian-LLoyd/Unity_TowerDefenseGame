using UnityEngine;

public class Target_Health : MonoBehaviour
{

    public float totalHealth = 100f;
    private float currentHealth;
    void Start()
    {
        currentHealth = totalHealth;

    }






    void Update()
    {
        
    }

    public void takeDamage(float damageToTake)
    {
        currentHealth -= damageToTake;

        if(currentHealth <=0)
        {
            gameObject.SetActive(false);
        }



    }

}
