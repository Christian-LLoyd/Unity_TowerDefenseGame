using UnityEngine;

public class TreeTransparency : MonoBehaviour
{
    private Renderer treeRenderer;
    private Color originalColor;
    public float fadeAmount = 0.3f; // Transparency when hiding
    public float detectRadius = 2f; // Adjust if needed

    void Start()
    {
        treeRenderer = GetComponent<Renderer>();
        if (treeRenderer != null)
        {
            originalColor = treeRenderer.material.color;
        }
    }

    void Update()
    {
        if (IsPlayerBehindTree())
        {
            SetTransparency(fadeAmount); // ✅ Fade out when player is behind
        }
        else
        {
            SetTransparency(1f); // ✅ Restore visibility when player moves away
        }
    }

    bool IsPlayerBehindTree()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) return false;

        Vector3 direction = player.transform.position - transform.position;
        RaycastHit hit;

        if (Physics.Raycast(transform.position, direction, out hit, detectRadius))
        {
            return hit.collider.CompareTag("Player");
        }
        return false;
    }

    void SetTransparency(float alpha)
    {
        if (treeRenderer != null)
        {
            Color newColor = originalColor;
            newColor.a = alpha;
            treeRenderer.material.color = newColor;
        }
    }
     // ✅ Draw Gizmos in the Scene view to visualize the detection radius
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow; // Set Gizmo color
        Gizmos.DrawWireSphere(transform.position, detectRadius); // Draw sphere around tree
    }
}
