using UnityEngine;

public class gizmoDebug : MonoBehaviour
{


    [SerializeField] private float spawnRadius = 5f;

    private void OnDrawGizmos(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);

    }

    private void OnDrawGizmosSelected()
    {
         
    }
}
