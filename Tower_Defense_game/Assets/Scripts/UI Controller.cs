using UnityEngine;
using UnityEngine.SceneManagement; 

public class UIController : MonoBehaviour
{   

    
    public static UIController instance;

    public GameObject levelCompleteScreen, levelFailedScreen;

    public void Awake()
    {
         Debug.Log("✅ UIController instance initialized successfully.");
        instance = this;
    }

    public void TryAgain() 
    {   
        Debug.Log("napindot ang try again");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
            
    }

    public void restart(){
        Time.timeScale = 1;
        SceneManager.LoadScene("Lvl_1");
    }
}
