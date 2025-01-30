using UnityEngine;

public class Enemy_Controller : MonoBehaviour
{

    public float moveSpeed;
    public Transform target;
    private Path thePath;
     private int currentPoint;
    private bool reachedEnd;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       thePath = FindAnyObjectByType<Path>();


    }

    // Update is called once per frame
    void Update()
    {
        if(!reachedEnd)
        {
            //transform.LookAt(thePath.points[currentPoint]);
            transform.position = Vector3.MoveTowards(transform.position, thePath.points[currentPoint].position, moveSpeed * Time.deltaTime);

        if(Vector3.Distance(transform.position, thePath.points[currentPoint].position) < 0.1f)
        {
            currentPoint = currentPoint + 1;
            if (currentPoint >= thePath.points.Length)
            {
                reachedEnd = true;
            }
        }

        }
    }
}
