using UnityEngine;

public class DebugCollider : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red; 
        Gizmos.DrawSphere(transform.position, 1);
    }
}
