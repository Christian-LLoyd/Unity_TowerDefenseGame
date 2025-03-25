using UnityEngine;

public class Target_Health : MonoBehaviour
{
    [Header("Health Settings")]
    public float totalHealth = 100f;
    private float currentHealth;

    void Start()
    {
        currentHealth = totalHealth;
    }

    public void TakeDamage(float damageToTake)
    {
        if (currentHealth <= 0) return; // Avoids taking damage when already "dead"

        currentHealth = Mathf.Max(currentHealth - damageToTake, 0); // Prevents negative health

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log($"{gameObject.name} has been destroyed!");
        gameObject.SetActive(false); // Efficiently handles object removal
    }
}
