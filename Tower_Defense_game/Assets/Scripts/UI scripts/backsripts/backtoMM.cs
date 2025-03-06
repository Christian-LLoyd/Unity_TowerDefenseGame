using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BacktoMM : MonoBehaviour
{
    //Load Scene
    public void Back_MM()
    {
        SceneManager.LoadScene("MainMenu");
    }

    //Quit Game
    public void Exit_Button()
    {
        Application.Quit();
        Debug.Log("The Player has Quit the game");
    }
}