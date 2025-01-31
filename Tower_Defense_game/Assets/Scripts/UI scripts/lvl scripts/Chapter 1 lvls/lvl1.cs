using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Chap1_lvl1 : MonoBehaviour
{
    //Load Scene
    public void Chap1_level1()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 5);
    }

    //Quit Game
    public void Exit_Button()
    {
        Application.Quit();
        Debug.Log("The Player has Quit the game");
    }
}