using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Chapter1_intro : MonoBehaviour
{
    //Load Scene
    public void Chapter_1_intro()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    //Quit Game
    public void Exit_Button()
    {
        Application.Quit();
        Debug.Log("The Player has Quit the game");
    }
}