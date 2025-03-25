using UnityEngine;

public class TreeTransparency : MonoBehaviour
{
    private Renderer treeRenderer;
    private Color originalColor;
    public float fadeAmount = 0.3f; // Transparency when hiding
    public float detectRadius = 2f; // Detection range for the player and enemy

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
        if (IsEntityBehindTree("Player") || IsEntityBehindTree("Enemy"))
        {
            SetTransparency(fadeAmount);
        //    Debug.Log($"🟡 {tag} detected nearby the tree.");

        }
        else
        {
            SetTransparency(1f);
        }
    }

    bool IsEntityBehindTree(string tag)
{
    GameObject[] entities = GameObject.FindGameObjectsWithTag(tag); // Handles multiple enemies or players
    foreach (GameObject entity in entities)
    {
        Vector3 direction = entity.transform.position - transform.position;
        RaycastHit hit;

        if (Physics.Raycast(transform.position, direction, out hit, detectRadius))
        {
            if (hit.collider.CompareTag(tag))
            {
                return true;
            }
        }
    }
    return false;
}


    void SetTransparency(float alpha)
    {
        if (treeRenderer != null)
        {
            Color newColor = treeRenderer.material.color;
            newColor.a = Mathf.Lerp(newColor.a, alpha, Time.deltaTime * 5f); // Smooth transition
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
