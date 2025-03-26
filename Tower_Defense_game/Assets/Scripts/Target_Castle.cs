using UnityEngine;

public class Target_Castle : MonoBehaviour
{
    public float Total_Health = 100f;
    public float Current_Health;
    private float _lastDamageTime;
    public float DamageCooldown = 0.2f;

    private HealthManager healthManager; // Reference to UI HealthManager

    void Start()
    {
        Current_Health = Total_Health;
        _lastDamageTime = -DamageCooldown;

        // Find and assign HealthManager
        healthManager = FindFirstObjectByType<HealthManager>();
        if (healthManager != null)
        {
            healthManager.SetHealth(Current_Health, Total_Health);
        }
    }

    public void Apply_Damage(float DamageToTake)
    {
        if (Time.time - _lastDamageTime < DamageCooldown)
        {
            Debug.Log("Damage cooldown active. Ignoring damage.");
            return;
        }

        Debug.Log("Castle is taking damage! Called by: " + gameObject.name);
        Current_Health -= DamageToTake;
        _lastDamageTime = Time.time;

        if (healthManager != null)
        {
            healthManager.SetHealth(Current_Health, Total_Health);
        }

        if (Current_Health <= 0)
        {
            Debug.Log("🔥Castle Destroyed!");
            Current_Health = 0;
            gameObject.SetActive(false);

            // Notify LevelManager that the castle is destroyed
            LevelManager.instance.CastleDestroyed();
        }
    }
}