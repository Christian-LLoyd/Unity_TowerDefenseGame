using UnityEngine;
using UnityEngine.UI; // Only if using a UI health bar

public class ParkHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    public Slider healthBar; // Drag the health bar UI here in Unity

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Parkmodel Health: " + currentHealth);
        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
            Debug.Log("The Parkmodel was destroyed!");
        }
    }

    void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            healthBar.value = (float)currentHealth / maxHealth;
        }
    }
}
