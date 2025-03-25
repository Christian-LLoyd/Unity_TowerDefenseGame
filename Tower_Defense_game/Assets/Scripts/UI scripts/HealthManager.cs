using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public Image healthBar;
    public float healthAmount = 100f;

    void Start()
    {
        UpdateHealthUI();
    }

    public void TakeDamage(float damage)
    {
        if (healthAmount <= 0) return; // Already at zero health, no further logic needed

        healthAmount = Mathf.Clamp(healthAmount - damage, 0, 100f);
        UpdateHealthUI();

        // If health reaches zero, trigger LevelManager's defeat logic
        if (healthAmount <= 0 && LevelManager.instance != null)
        {
            LevelManager.instance.CastleDestroyed();
        }
    }

    public void SetHealth(float currentHealth, float maxHealth)
    {
        healthAmount = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (healthBar != null)
        {
            healthBar.fillAmount = healthAmount / maxHealth;
        }
    }

    private void UpdateHealthUI()
    {
        if (healthBar != null)
        {
            healthBar.fillAmount = healthAmount / 100f;
        }
    }
}
