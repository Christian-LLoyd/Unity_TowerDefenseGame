using UnityEngine;

public class TreeTransparency : MonoBehaviour
{
    private Material material;
    private Color originalColor;
    private bool isTransparent = false;
    public float transparencyLevel = 0.5f; // Adjust transparency (0 = fully transparent, 1 = opaque)

    void Start()
    {
        // Get the tree's material
        material = GetComponent<Renderer>().material;
        originalColor = material.color;
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Something entered the trigger: " + other.gameObject.name); // Debug log
        // Check if the collider belongs to an enemy
        if (other.CompareTag("Enemy"))
        {
            SetTransparency(transparencyLevel);
            SetTransparency(0.5f);
            isTransparent = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Restore visibility when enemy leaves
        if (other.CompareTag("Enemy") && isTransparent)
        {
            SetTransparency(1f);
            isTransparent = false;
        }
    }

    void SetTransparency(float alpha)
    {
        Color newColor = originalColor;
        newColor.a = alpha; // Adjust alpha value
        material.color = newColor;
    }
}
