using UnityEngine;

public class Enemy_Controller : MonoBehaviour
{
    public float moveSpeed;
    public Transform target;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Move towards the target
        transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

        
        Vector3 direction = (target.position - transform.position).normalized;
        if (direction != Vector3.zero) 
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 10f * Time.deltaTime);
        }
    }
}
