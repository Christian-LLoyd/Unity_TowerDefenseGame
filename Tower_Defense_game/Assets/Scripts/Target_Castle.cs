using UnityEngine;

public class Target_Castle : MonoBehaviour
{

    public float Total_Health = 100f;
    public float Current_Health;
    void Start()
    {
        Current_Health = Total_Health;
    }

    void Update()
    {
    
    }

    public void Apply_Damage(float DamageToTake)
    {
        Current_Health -= DamageToTake;

        if(Current_Health <=0)
        {
            gameObject.SetActive(false);
        }
    }
}
