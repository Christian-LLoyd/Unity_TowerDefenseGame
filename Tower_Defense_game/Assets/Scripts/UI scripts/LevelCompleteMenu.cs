using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class LevelComplete : MonoBehaviour
{

    [SerializeField] GameObject Lvl_complete;
    public void Complete_Level()
    {
        Lvl_complete.SetActive(true);
        Time.timeScale = 0;
    }

    public void Next_Level()
    {
                
        SceneManager.LoadScene("Lvl_2");

    }


}
